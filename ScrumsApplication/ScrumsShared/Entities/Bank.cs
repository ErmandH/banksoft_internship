using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsShared.Entities
{
	public class Bank
	{
		public int BankID { get; set; }
		public string BankCode { get; set; }
		public string BankName { get; set; }

		public static Bank CreateBankFromDataRow(DataRow row)
		{
			Bank bank = new Bank();
			bank.BankID = row.Field<int>("BankID");
			bank.BankCode = row.Field<string>("BankCode");
			bank.BankName = row.Field<string>("BankName");
			return bank;
		}

		public static IList<Bank> CreateBanksFromDataTable(DataTable table)
		{
			IList<Bank> banks = new List<Bank>();
			foreach (DataRow row in table.Rows)
			{
				banks.Add(Bank.CreateBankFromDataRow(row));
			}
			return banks;
		}
	}

}
