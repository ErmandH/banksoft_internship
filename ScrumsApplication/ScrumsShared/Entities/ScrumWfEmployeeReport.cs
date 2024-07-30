using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsShared.Entities
{
	public class ScrumWfEmployeeReport
	{
		public int? WFNo { get; set; }
		public string EmployeeCode { get; set; }
		public decimal? CompletedSum { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }

		public static ScrumWfEmployeeReport CreateReportFromDataRow(DataRow row)
		{
			return new ScrumWfEmployeeReport
			{
				WFNo = row.Field<int?>("WFNo"),
				EmployeeCode = row.Field<string>("EmployeeCode"),
				CompletedSum = row.Field<decimal?>("CompletedSum"),
				StartDate = row.Field<DateTime?>("StartDate"),
				FinishDate = row.Field<DateTime?>("FinishDate")
			};
		}

		public static IList<ScrumWfEmployeeReport> CreateReportFromDataTable(DataTable table)
		{
			IList<ScrumWfEmployeeReport> reports = new List<ScrumWfEmployeeReport>();
			foreach (DataRow row in table.Rows)
			{
				reports.Add(CreateReportFromDataRow(row));
			}
			return reports;
		}
	}
}
