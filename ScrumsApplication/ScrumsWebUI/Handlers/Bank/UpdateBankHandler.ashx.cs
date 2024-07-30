using Newtonsoft.Json;
using ScrumsServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ScrumsWeb.Handlers.Bank
{
	/// <summary>
	/// Summary description for UpdateEmployeeHandler
	/// </summary>
	public class UpdateBankHandler : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			try
			{
				string jsonString;
				using (var reader = new StreamReader(context.Request.InputStream))
				{
					jsonString = reader.ReadToEnd();
				}
				ScrumsShared.Entities.Bank updatedRow = JsonConvert.DeserializeObject<ScrumsShared.Entities.Bank>(jsonString);

				BankManager bankManager = new BankManager();
				bankManager.UpdateBank(updatedRow);
				context.Response.StatusCode = 200;
				context.Response.Write("{\"status\":\"success\"}");
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				context.Response.Write("{\"status\":\"error\", \"message\":\"" + ex.Message + "\"}");
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}