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
	/// Summary description for InsertEmployeeHandler
	/// </summary>
	public class InsertBankHandler : IHttpHandler
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
				ScrumsShared.Entities.Bank[] banks = JsonConvert.DeserializeObject<ScrumsShared.Entities.Bank[]>(jsonString);


				BankManager bankManager = new BankManager();
				foreach (var bank in banks)
				{
					bankManager.InsertBank(bank);
				}
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