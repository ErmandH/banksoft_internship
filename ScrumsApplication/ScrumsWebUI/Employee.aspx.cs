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
	public partial class Employee : System.Web.UI.Page
	{
		private readonly EmployeeManager empManager;
		public Employee()
		{
			this.empManager = new EmployeeManager();
		}
		private void LoadGridView()
		{
			DataTable table = empManager.GetAllEmployees();

			var data = ScrumsShared.Entities.Employee.CreateEmployeesFromDataTable(table);
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