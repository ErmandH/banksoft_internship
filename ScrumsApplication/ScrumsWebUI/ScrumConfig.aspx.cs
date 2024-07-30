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
	public partial class ScrumConfig : System.Web.UI.Page
	{
		private readonly ScrumConfigManager configManager;
		public ScrumConfig()
		{
			this.configManager = new ScrumConfigManager();
		}
		private void LoadGridView()
		{
			DataTable table = configManager.GetAllScrumConfigs();

			var data = ScrumsShared.Entities.ScrumConfig.CreateScrumConfigsFromDataTable(table);
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