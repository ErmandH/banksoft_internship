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
	public class BankManager
	{
		private DbManager dbManager;

		public BankManager()
		{
			dbManager = new DbManager();
		}

		public DataTable GetAllBanks()
		{
			DataTable table = dbManager.ExecuteQuery("scsp_GetAllBanks");
			return table;
		}

		public void UpdateBank(Bank updatedBank)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@BankID", updatedBank.BankID),
            new SqlParameter("@BankCode", updatedBank.BankCode),
            new SqlParameter("@BankName", updatedBank.BankName)
        };
			dbManager.ExecuteCommand("scsp_UpdateBank", parameters);
		}

		public void InsertBank(Bank bank)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@BankCode", bank.BankCode),
            new SqlParameter("@BankName", bank.BankName)
        };
			dbManager.ExecuteCommand("scsp_InsertBank", parameters);
		}

		public void DeleteBank(int bankId)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@BankID", bankId)
        };
			dbManager.ExecuteCommand("scsp_DeleteBank", parameters);
		}
	}

}
