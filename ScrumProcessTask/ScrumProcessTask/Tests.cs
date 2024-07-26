////temp
//			int count = 0;

//			// worksheetleri tek tek okuyarak veritabanına ekle
//			foreach (Worksheet worksheet in sheets)
//			{
//				// sil
//				if (count == 3) break;

//				if (worksheet.Name == "Grafik" || worksheet.Name == "Chart")
//					continue;
//				Console.WriteLine(worksheet.Name);
//				int rowCount = worksheet.UsedRange.Rows.Count;
//				int colCount = worksheet.UsedRange.Columns.Count;

//				// son satırda istatikstikler olduğu için row <= rowCount yerine row < rowCount kullanıyorum
//				for (int row = 2; row < rowCount; row++)
//					InsertScrumsToDb(worksheet, scrumConfig, row, filename);

//				count++; // sil
//			}


////---------------------------------------------
//// worksheetleri tek tek okuyarak veritabanına ekle
//			foreach (Worksheet worksheet in sheets)
//			{
//				if (worksheet.Name == "Grafik" || worksheet.Name == "Chart")
//					continue;

//				if (worksheet.Name != "5_12Temmuz")
//					continue;
//				Console.WriteLine(worksheet.Name);
//				int rowCount = worksheet.UsedRange.Rows.Count;
//				int colCount = worksheet.UsedRange.Columns.Count;

//				// son satırda istatikstikler olduğu için row <= rowCount yerine row < rowCount kullanıyorum
//				for (int row = 2; row < rowCount; row++)
//					InsertScrumsToDb(worksheet, scrumConfig, row, filename);
//			}




//private void InsertScrumsToDb(Worksheet worksheet, ScrumConfig config, int row, string fileName)
//		{

//			// satırın başlangıç sütun değeri
//			decimal start = Convert.ToDecimal(worksheet.Cells[row, config.StartColNo].Value);

//			// Tarihlerin başladığı sütun
//			int dateStartCol = config.DateStartColNo;

//			string dateStr = ((DateTime)worksheet.Cells[ExcelSheetEnum.HeaderRow, dateStartCol].Value).ToShortDateString();

//			// sütun değeri tarih iken devam ettir döngüyü
//			while (worksheet.Cells[ExcelSheetEnum.HeaderRow, dateStartCol].Value is DateTime && worksheet.Cells[row, dateStartCol].Value != null)
//			{
//				DateTime date = Regex.Replace(worksheet.Cells[ExcelSheetEnum.HeaderRow, dateStartCol].Value, @"\s+", "");
//				// aynı takıma ait aynı günde daha önce bir kayıt varsa o kayıtları sil
//				this.DeleteIfDateExists(date, config.TeamCode);

//				// Tarihe kadar olan parametreleri ekle
//				List<SqlParameter> parameters = AppendParameters(worksheet, config, row, fileName);

//				SqlParameter dateParam = parameters.Where(p => p.ParameterName == "@Date").FirstOrDefault();
//				SqlParameter completedParam = parameters.Where(p => p.ParameterName == "@Completed").FirstOrDefault();

//				// daha önce remainingParam eklenmediyse ekle
//				if (dateParam == null)
//					parameters.Add(new SqlParameter("@Date", date));
//				else // var ise değerini değiştir
//					dateParam.Value = date;

//				// geriye kalan iş günü
//				decimal remainingWork = Convert.ToDecimal(worksheet.Cells[row, dateStartCol].Value);

//				// başlangıç - geriye kalan iş gününden o gün tamamlanmış iş gününü öğreniyorum
//				decimal completedWork = start - remainingWork;

//				// bir sonraki gün için startı önceki günden kalan iş günüyle değiştiriyorum
//				start = remainingWork;

//				// daha önce completedParam eklenmediyse ekle
//				if (completedParam == null)
//					parameters.Add(new SqlParameter("@Completed", completedWork));
//				else // var ise değerini değiştir
//					completedParam.Value = completedWork;

//				// veritabanına insert işlemi
//				dalKrediKartlari.ExecuteFromXmlQuery("InsertScrum", parameters.ToArray());
//				dateStartCol++;
//			}





//		}



//-------------------------------
//private void DeleteIfDateExists(DateTime date, DateTime scrumDate ,string teamCode, double wfno, string employee)
//		{
//			System.Data.DataTable table = (System.Data.DataTable)dalKrediKartlari
//											.ExecuteFromXmlQuery("GetScrumsByDate",
//												new SqlParameter("@Date", date),
//												new SqlParameter("@TeamCode", teamCode),
//												new SqlParameter("@WFNo", wfno),
//												new SqlParameter("@Employee", employee)
//											);
//			if (table.Rows.Count < 1)
//				return;
//			IList<Scrum> scrums = Scrum.CreateScrumsFromDataTable(table);
//			foreach (var scrum in scrums)
//			{
//				dalKrediKartlari.ExecuteFromXmlQuery("DeleteScrum", new SqlParameter("@ScrumID", scrum.ScrumID));
//				Console.WriteLine(string.Format("Deleting: {0} {1} {2} {3} {4}", scrum.ScrumID, scrum.TeamCode, scrum.Employee ,scrum.ScrumStartDate.ToShortDateString(), scrum.Date.ToShortDateString()));
//			}
//		}