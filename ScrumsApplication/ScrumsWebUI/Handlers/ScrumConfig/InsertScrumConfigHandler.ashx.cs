using Newtonsoft.Json;
using ScrumsServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ScrumsWeb.Handlers
{
	/// <summary>
	/// Summary description for InsertScrumConfigHandler
	/// </summary>
	public class InsertScrumConfigHandler : IHttpHandler
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
				ScrumsShared.Entities.ScrumConfig[] configs = JsonConvert.DeserializeObject<ScrumsShared.Entities.ScrumConfig[]>(jsonString);

				ScrumConfigManager configManager = new ScrumConfigManager();
				foreach (var config in configs)
				{
					configManager.InsertScrumConfig(config);
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
			get { return false; }
		}
	}
}