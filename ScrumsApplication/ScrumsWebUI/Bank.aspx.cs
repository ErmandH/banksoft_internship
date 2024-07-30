using Newtonsoft.Json;
using ScrumsServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScrumsWeb
{
	public partial class Bank : System.Web.UI.Page
	{
		private readonly BankManager bankManager;
		public Bank()
		{
			this.bankManager = new BankManager();
		}
		private void LoadGridView()
		{
			DataTable table = bankManager.GetAllBanks();

			var data = ScrumsShared.Entities.Bank.CreateBanksFromDataTable(table);
			var jsonSettings = new JsonSerializerSettings();
			jsonSettings.DateFormatString = "yyyy-MM-dd";
			string ScrumsJsonData = JsonConvert.SerializeObject(data, jsonSettings);
			// JavaScript kodunu sayfaya ekle
			string script = string.Format("initializeHandsontable({0});", ScrumsJsonData);
			ClientScript.RegisterStartupScript(this.GetType(), "initializeHandsontableScript", script, true);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadGridView();
			}
		}
	}
}