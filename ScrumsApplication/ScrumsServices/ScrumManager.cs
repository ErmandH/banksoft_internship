using Microsoft.Office.Interop.Excel;
using ScrumsShared.Entities;
using ScrumsShared.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsServices
{
    public class ScrumManager
    {
        private DbManager dbManager;

        public ScrumManager()
        {
            this.dbManager = new DbManager();
        }


        public System.Data.DataTable GetAllScrums()
        {
            var scrumTable = dbManager.ExecuteQuery("scsp_GetAllScrums");
            return scrumTable;
        }

		public System.Data.DataTable GetAllScrumsFiltered(string employeeCode, string bankCode, string startDate, string endDate)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@TarihBasla", startDate),
				new SqlParameter("@TarihBitir", endDate),
				new SqlParameter("@Ilgili", employeeCode),
				new SqlParameter("@BankaKodu", bankCode),
			};
			var scrumTable = dbManager.ExecuteQuery("scsp_GetAllScrumsFiltered", parameters);
			return scrumTable;
		}

		public System.Data.DataTable GetWfTimeReports(string employeeCode, string bankCode, string startDate, string endDate)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@TarihBasla", startDate),
				new SqlParameter("@TarihBitir", endDate),
				new SqlParameter("@Ilgili", employeeCode),
				new SqlParameter("@BankaKodu", bankCode),
			};
			var scrumTable = dbManager.ExecuteQuery("rpsp_GetWfTimeReports", parameters);
			return scrumTable;
		}

		public System.Data.DataTable GetWfEmployeeReports(string employeeCode, string bankCode, string startDate, string endDate)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@TarihBasla", startDate),
				new SqlParameter("@TarihBitir", endDate),
				new SqlParameter("@Ilgili", employeeCode),
				new SqlParameter("@BankaKodu", bankCode),
			};
			var scrumTable = dbManager.ExecuteQuery("rpsp_GetWfEmployeeTimeReports", parameters);
			return scrumTable;
		}
    }
}
