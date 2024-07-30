using ScrumsShared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsServices
{
	public class ScrumConfigManager
	{
		private DbManager dbManager;

		public ScrumConfigManager()
		{
			dbManager = new DbManager();
		}

		public DataTable GetAllScrumConfigs()
		{
			DataTable table = dbManager.ExecuteQuery("scsp_GetAllScrumConfigs");
			return table;
		}

		public void UpdateScrumConfig(ScrumConfig updatedRow)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@ScrumConfigID", updatedRow.ScrumConfigID),
				new SqlParameter("@TeamCode", updatedRow.TeamCode),
				new SqlParameter("@WFNoColNo", updatedRow.WFNoColNo),
				new SqlParameter("@BankColNo", updatedRow.BankColNo),
				new SqlParameter("@SubjectColNo", updatedRow.SubjectColNo),
				new SqlParameter("@EmployeeColNo", updatedRow.EmployeeColNo),
				new SqlParameter("@DescriptionColNo", updatedRow.DescriptionColNo),
				new SqlParameter("@PriorityColNo", updatedRow.PriorityColNo),
				new SqlParameter("@StatusColNo", updatedRow.StatusColNo),
				new SqlParameter("@StartColNo", updatedRow.StartColNo),
				new SqlParameter("@DateStartColNo", updatedRow.DateStartColNo),
				new SqlParameter("@DataStartRowNo", updatedRow.DataStartRowNo)
			};
			dbManager.ExecuteCommand("scsp_UpdateScrumConfig", parameters);
		}

		public void InsertScrumConfig(ScrumConfig config)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@TeamCode", config.TeamCode),
				new SqlParameter("@WFNoColNo", config.WFNoColNo),
				new SqlParameter("@BankColNo", config.BankColNo),
				new SqlParameter("@SubjectColNo", config.SubjectColNo),
				new SqlParameter("@EmployeeColNo", config.EmployeeColNo),
				new SqlParameter("@DescriptionColNo", config.DescriptionColNo),
				new SqlParameter("@PriorityColNo", config.PriorityColNo),
				new SqlParameter("@StatusColNo", config.StatusColNo),
				new SqlParameter("@StartColNo", config.StartColNo),
				new SqlParameter("@DateStartColNo", config.DateStartColNo),
				new SqlParameter("@DataStartRowNo", config.DataStartRowNo),
			};
			dbManager.ExecuteCommand("scsp_InsertScrumConfig", parameters);
		}

		public void DeleteScrumConfig(int configId)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@ScrumConfigID", configId),
			};
			dbManager.ExecuteCommand("scsp_DeleteScrumConfig", parameters);
		}
	}
}
