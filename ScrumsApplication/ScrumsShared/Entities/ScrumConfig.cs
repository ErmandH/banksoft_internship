using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsShared.Entities
{
	public class ScrumConfig
	{
		public int ScrumConfigID { get; set; }
		public string TeamCode { get; set; }
		public int WFNoColNo { get; set; }
		public int BankColNo { get; set; }
		public int SubjectColNo { get; set; }
		public int EmployeeColNo { get; set; }
		public int DescriptionColNo { get; set; }
		public int PriorityColNo { get; set; }
		public int StatusColNo { get; set; }
		public int StartColNo { get; set; }
		public int DateStartColNo { get; set; }
		public int DataStartRowNo { get; set; }
		public DateTime? InsertDate { get; set; }

		public static ScrumConfig CreateScrumConfigFromDataRow(DataRow row)
		{
			ScrumConfig config = new ScrumConfig();
			config.ScrumConfigID = row.Field<int>("ScrumConfigID");
			config.TeamCode = row.Field<string>("TeamCode");
			config.WFNoColNo = row.Field<int>("WFNoColNo");
			config.BankColNo = row.Field<int>("BankColNo");
			config.SubjectColNo = row.Field<int>("SubjectColNo");
			config.EmployeeColNo = row.Field<int>("EmployeeColNo");
			config.DescriptionColNo = row.Field<int>("DescriptionColNo");
			config.PriorityColNo = row.Field<int>("PriorityColNo");
			config.StatusColNo = row.Field<int>("StatusColNo");
			config.StartColNo = row.Field<int>("StartColNo");
			config.DateStartColNo = row.Field<int>("DateStartColNo");
			config.DataStartRowNo = row.Field<int>("DataStartRowNo");
			config.InsertDate = row.Field<DateTime?>("InsertDate");
			return config;
		}

		public static IList<ScrumConfig> CreateScrumConfigsFromDataTable(DataTable table)
		{
			IList<ScrumConfig> scrums = new List<ScrumConfig>();
			foreach (DataRow row in table.Rows)
			{
				scrums.Add(ScrumConfig.CreateScrumConfigFromDataRow(row));
			}
			return scrums;
		}
	}
}
