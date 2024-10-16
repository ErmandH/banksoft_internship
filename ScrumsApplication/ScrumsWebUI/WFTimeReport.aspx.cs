﻿using Newtonsoft.Json;
using ScrumsServices;
using ScrumsShared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScrumsWeb
{
	public partial class WFTimeReport : System.Web.UI.Page
	{
		private readonly ScrumManager scrumManager;
		private readonly BankManager bankManager;
		private readonly EmployeeManager empManager;

		public WFTimeReport()
        {
            scrumManager = new ScrumManager();
			bankManager = new BankManager();
			empManager = new EmployeeManager();
        }

        private void LoadGridView(string employeeCode, string bankCode, string startDate, string endDate)
        {
            DataTable table = scrumManager.GetWfTimeReports(employeeCode, bankCode, startDate, endDate);

            //var data = ScrumWfTimeReport.CreateReportFromDataTable(table);
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "yyyy-MM-dd";
            string ScrumsJsonData = JsonConvert.SerializeObject(table, jsonSettings);
             // JavaScript kodunu sayfaya ekle
            string script = string.Format("initializeHandsontable({0});", ScrumsJsonData);
            ClientScript.RegisterStartupScript(this.GetType(), "initializeHandsontableScript", script, true);
        }

		private void BindBanks()
		{
			var banks = bankManager.GetAllBanks();
			ddlBanks.DataSource = banks;
			ddlBanks.DataTextField = "BankName";
			ddlBanks.DataValueField = "BankCode";
			ddlBanks.DataBind();
			ddlBanks.Items.Insert(0, new ListItem("--Banka Seçin--", ""));
		}

		private void BindEmployees()
		{
			var employees = empManager.GetAllEmployees();
			ddlEmployees.DataSource = employees;
			ddlEmployees.DataTextField = "EmployeeName";
			ddlEmployees.DataValueField = "EmployeeCode";
			ddlEmployees.DataBind();
			ddlEmployees.Items.Insert(0, new ListItem("--İlgili Seçin--", ""));
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				BindBanks();
				BindEmployees();
            }
        }



		protected void btnFilter_Click(object sender, EventArgs e)
		{
			try
			{
				string formattedStartDate = "";
				string formattedEndDate = "";
				string empCode = ddlEmployees.SelectedValue;
				string bankCode = ddlBanks.SelectedValue;
				if (!string.IsNullOrEmpty(txtStartDate.Text)) 
				{
					DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
					formattedStartDate = startDate.ToString("yyyyMMdd");
				}

				if (!string.IsNullOrEmpty(txtEndDate.Text))
				{
					DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
					formattedEndDate = endDate.ToString("yyyyMMdd");
				}
				LoadGridView(empCode, bankCode, formattedStartDate, formattedEndDate);
			}
			catch (Exception ex)
			{
				string script = string.Format("toastr.error('{0}');", ex.Message);
				toastrErrScript.Text = string.Format("<script type=\"text/javascript\">{0}</script>", script);
			}
			
		}
	}
}