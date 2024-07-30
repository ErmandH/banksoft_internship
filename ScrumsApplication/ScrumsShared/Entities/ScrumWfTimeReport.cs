using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsShared.Entities
{
	public class ScrumWfTimeReport
	{
		public int? WFNo { get; set; }
		public string BankCode { get; set; }
		public decimal? CompletedSum { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }

		public static ScrumWfTimeReport CreateReportFromDataRow(DataRow row)
		{
			return new ScrumWfTimeReport
			{
				WFNo = row.Field<int?>("WFNo"),
				BankCode = row.Field<string>("BankCode"),
				CompletedSum = row.Field<decimal?>("CompletedSum"),
				StartDate = row.Field<DateTime?>("StartDate"),
				FinishDate = row.Field<DateTime?>("FinishDate")
			};
		}

		public static IList<ScrumWfTimeReport> CreateReportFromDataTable(DataTable table)
		{
			IList<ScrumWfTimeReport> reports = new List<ScrumWfTimeReport>();
			foreach (DataRow row in table.Rows)
			{
				reports.Add(CreateReportFromDataRow(row));
			}
			return reports;
		}
	}
}
