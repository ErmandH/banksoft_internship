using Banksoft.DAL;
using Banksoft.Logging;
using Microsoft.Office.Interop.Excel;
using ScrumProcessTask.Entities;
using ScrumProcessTask.Enums;
using ScrumProcessTask.Utilites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrumProcessTask.Services
{
	public class ScrumManager
	{
		private BaseDAL dalKrediKartlari;
		private ScrumConfigManager configManager;

		public ScrumManager(BaseDAL baseDAL)
		{
			this.dalKrediKartlari = baseDAL;
			this.configManager = new ScrumConfigManager(baseDAL);
		}

		private void DeleteIfDateExists(Worksheet worksheet, ScrumConfig config, string teamCode)
		{
			int dateStartCol = config.DateStartColNo;

			while (ScrumOperations.GetExcelDate(worksheet.Cells[ExcelSheetEnum.HeaderRow, dateStartCol].Value) != null)
			{
				DateTime date = ScrumOperations.GetExcelDate(worksheet.Cells[ExcelSheetEnum.HeaderRow, dateStartCol].Value);
				// aynı takıma ait aynı günde daha önce bir kayıt varsa o kayıtları sil
				System.Data.DataTable table = (System.Data.DataTable)dalKrediKartlari
											.ExecuteFromXmlQuery("GetScrumsByDate",
												new SqlParameter("@WorkDate", date),
												new SqlParameter("@TeamCode", teamCode)
											);
				if (table.Rows.Count < 1)
					return;
				IList<Scrum> scrums = Scrum.CreateScrumsFromDataTable(table);
				foreach (var scrum in scrums)
				{
					dalKrediKartlari.ExecuteFromXmlQuery("DeleteScrum", new SqlParameter("@ScrumID", scrum.ScrumID));
					Console.WriteLine(string.Format("Deleting: {0} {1} {2} {3} {4}", scrum.ScrumID, scrum.TeamCode, scrum.EmployeeCode, scrum.ScrumStartDate.ToString(), scrum.WorkDate.ToString()));
				}
				dateStartCol++;
			}
		}

		private List<SqlParameter> AppendParameters(Worksheet worksheet, ScrumConfig config, int row, string fileName)
		{
			List<SqlParameter> parameters = new List<SqlParameter>();

			parameters.Add(new SqlParameter("@TeamCode", config.TeamCode));
			parameters.Add(new SqlParameter("@Filename", fileName));

			// Tarihlerin başladığı sütun
			parameters.Add(new SqlParameter("@ScrumStartDate", worksheet.Cells[ExcelSheetEnum.HeaderRow, config.DateStartColNo].Value));
			parameters.Add(new SqlParameter("@WFNo", ScrumOperations.HandleDoubleColumn(worksheet.Cells[row, config.WFNoColNo].Value) ?? DBNull.Value));
			parameters.Add(new SqlParameter("@BankCode", worksheet.Cells[row, config.BankColNo].Value ?? DBNull.Value));
			parameters.Add(new SqlParameter("@Subject", worksheet.Cells[row, config.SubjectColNo].Value ?? DBNull.Value));
			parameters.Add(new SqlParameter("@EmployeeCode", worksheet.Cells[row, config.EmployeeColNo].Value ?? DBNull.Value));
			parameters.Add(new SqlParameter("@Description", worksheet.Cells[row, config.DescriptionColNo].Value ?? DBNull.Value));
			parameters.Add(new SqlParameter("@Priority", ScrumOperations.HandleDoubleColumn(worksheet.Cells[row, config.PriorityColNo].Value) ?? DBNull.Value));
			parameters.Add(new SqlParameter("@Status", worksheet.Cells[row, config.StatusColNo].Value ?? DBNull.Value));
			parameters.Add(new SqlParameter("@Start", ScrumOperations.HandleDoubleColumn(worksheet.Cells[row, config.StartColNo].Value) ?? DBNull.Value));

			return parameters;
		}

		private void InsertScrumsToDb(Worksheet worksheet, ScrumConfig config, int row, string fileName)
		{
			try
			{
				// satırın başlangıç sütun değeri
				decimal start = Convert.ToDecimal(worksheet.Cells[row, config.StartColNo].Value);

				// Tarihlerin başladığı sütun
				int dateStartCol = config.DateStartColNo;

				// sütun değeri tarih iken devam ettir döngüyü
				while (ScrumOperations.GetExcelDate(worksheet.Cells[ExcelSheetEnum.HeaderRow, dateStartCol].Value) != null && worksheet.Cells[row, dateStartCol].Value != null)
				{
					DateTime date = ScrumOperations.GetExcelDate(worksheet.Cells[ExcelSheetEnum.HeaderRow, dateStartCol].Value);

					// Tarihe kadar olan parametreleri ekle
					List<SqlParameter> parameters = AppendParameters(worksheet, config, row, fileName);

					SqlParameter dateParam = parameters.Where(p => p.ParameterName == "@WorkDate").FirstOrDefault();
					SqlParameter completedParam = parameters.Where(p => p.ParameterName == "@Completed").FirstOrDefault();

					// daha önce remainingParam eklenmediyse ekle
					if (dateParam == null)
						parameters.Add(new SqlParameter("@WorkDate", date));
					else // var ise değerini değiştir
						dateParam.Value = date;

					// geriye kalan iş günü
					decimal remainingWork = Convert.ToDecimal(worksheet.Cells[row, dateStartCol].Value);

					// başlangıç - geriye kalan iş gününden o gün tamamlanmış iş gününü öğreniyorum
					decimal completedWork = start - remainingWork;

					// bir sonraki gün için startı önceki günden kalan iş günüyle değiştiriyorum
					start = remainingWork;

					// daha önce completedParam eklenmediyse ekle
					if (completedParam == null)
						parameters.Add(new SqlParameter("@Completed", completedWork));
					else // var ise değerini değiştir
						completedParam.Value = completedWork;

					// veritabanına insert işlemi
					dalKrediKartlari.ExecuteFromXmlQuery("InsertScrum", parameters.ToArray());
					dateStartCol++;
				}
			}
			catch (Exception ex)
			{
				Logger.Log2ErrorFile(string.Format("ScrumProcessTask->ScrumManager->InsertScrumsToDb(): Filename:{0} Sheet:{1} Row:{2} Exception:{3}",
													fileName, worksheet.Name, row, ex.ToString()));
				throw ex;
			}
		}

		public void InsertScrumsFromExcelFile(string filePath)
		{
			string filename = Path.GetFileName(filePath);
			Application excelApp = new Application();
			Workbook workbook = excelApp.Workbooks.Open(filePath);
			try
			{
				string teamCode = ScrumOperations.GetTeamCode(Path.GetFileNameWithoutExtension(filePath));
				ScrumConfig scrumConfig = this.configManager.GetScrumConfigByTeam(teamCode);
				if (scrumConfig == null)
					throw new NullReferenceException("Scrum config is null");

				// Sheets koleksiyonunu tersine çevir
				List<Worksheet> sheets = new List<Worksheet>();
				foreach (Worksheet sheet in workbook.Worksheets)
					sheets.Add(sheet);
				sheets.Reverse();

				// worksheetleri tek tek okuyarak veritabanına ekle
				foreach (Worksheet worksheet in sheets)
				{
					if (worksheet.Name == "Grafik" || worksheet.Name == "Chart")
						continue;

					Console.WriteLine(Path.GetFileNameWithoutExtension(filePath) + " -> " + "Processing: " + worksheet.Name);
					Console.WriteLine("---------------------------------");
					try
					{
						this.DeleteIfDateExists(worksheet, scrumConfig, teamCode);
					}
					catch (Exception ex)
					{
						Logger.Log2ErrorFile(string.Format("ScrumProcessTask->ScrumManager->DeleteIfDateExists(): Filename:{0} Sheet:{1} Exception:{2}",
														filename, worksheet.Name, ex.ToString()));
						throw ex;
					}

					int rowCount = worksheet.UsedRange.Rows.Count;
					int colCount = worksheet.UsedRange.Columns.Count;
					// son satırda istatikstikler olduğu için row <= rowCount yerine row < rowCount kullanıyorum
					for (int row = scrumConfig.DataStartRowNo; row <= rowCount; row++)
						InsertScrumsToDb(worksheet, scrumConfig, row, filename);
					Console.WriteLine("---------------------------------");
				}
			}
			catch (Exception ex)
			{
				Logger.Log2ErrorFile(string.Format("ScrumProcessTask->ScrumManager->DeleteIfDateExists(): Filename:{0} Sheet:{1} Exception:{2}",
													filename, workbook.Name, ex.ToString()));
				throw;
			}
			finally
			{
				workbook.Close(false);
				System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
				excelApp.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
			}
		}
	}
}
