using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsShared.Entities
{
	public class Scrum
	{
		public int ScrumID { get; set; }
		public string TeamCode { get; set; }
		public string Filename { get; set; }
		public DateTime ScrumStartDate { get; set; }
		public int? WFNo { get; set; }
		public string BankCode { get; set; }
		public string Subject { get; set; }
		public string EmployeeCode { get; set; }
		public string Description { get; set; }
		public decimal? Priority { get; set; }
		public string Status { get; set; }
		public decimal? Start { get; set; }
		public decimal? Completed { get; set; }
		public DateTime? WorkDate { get; set; }
		public DateTime? InsertDate { get; set; }

		public static Scrum CreateScrumFromDataRow(DataRow row)
		{
			Scrum scrum = new Scrum();
			scrum.ScrumID = row.Field<int>("ScrumID");
			scrum.TeamCode = row.Field<string>("TeamCode");
			scrum.Filename = row.Field<string>("Filename");
			scrum.ScrumStartDate = row.Field<DateTime>("ScrumStartDate");
			scrum.WFNo = row.Field<int?>("WFNo");
			scrum.BankCode = row.Field<string>("BankCode");
			scrum.Subject = row.Field<string>("Subject");
			scrum.EmployeeCode = row.Field<string>("EmployeeCode");
			scrum.Description = row.Field<string>("Description");
			scrum.Priority = row.Field<decimal?>("Priority");
			scrum.Status = row.Field<string>("Status");
			scrum.Start = row.Field<decimal?>("Start");
			scrum.Completed = row.Field<decimal?>("Completed");
			scrum.WorkDate = row.Field<DateTime?>("WorkDate");
			scrum.InsertDate = row.Field<DateTime?>("InsertDate");
			return scrum;
		}

		public static IList<Scrum> CreateScrumsFromDataTable(DataTable table)
		{
			IList<Scrum> scrums = new List<Scrum>();
			foreach (DataRow row in table.Rows)
			{
				scrums.Add(Scrum.CreateScrumFromDataRow(row));
			}
			return scrums;
		}
	}


}
