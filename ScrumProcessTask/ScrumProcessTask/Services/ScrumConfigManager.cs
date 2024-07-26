using Banksoft.DAL;
using ScrumProcessTask.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumProcessTask.Services
{
	public class ScrumConfigManager
	{
		private BaseDAL dalKrediKartlari;

		public ScrumConfigManager(BaseDAL baseDAL)
        {
            this.dalKrediKartlari = baseDAL;
        }

		public ScrumConfig GetScrumConfigByTeam(string teamCode) 
		{
			DataTable table = (DataTable) dalKrediKartlari.ExecuteFromXmlQuery("GetScrumConfigByTeam", new SqlParameter("@TeamCode", teamCode));
			if (table.Rows.Count < 1)
				return null;
			return ScrumConfig.CreateScrumConfigFromDataRow(table.Rows[0]);
		}
	}
}
