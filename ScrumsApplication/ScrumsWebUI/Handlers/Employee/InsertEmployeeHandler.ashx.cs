using Newtonsoft.Json;
using ScrumsServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ScrumsWeb.Handlers.Employee
{
	/// <summary>
	/// Summary description for InsertEmployeeHandler
	/// </summary>
	public class InsertEmployeeHandler : IHttpHandler
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
				ScrumsShared.Entities.Employee[] employees = JsonConvert.DeserializeObject<ScrumsShared.Entities.Employee[]>(jsonString);

				EmployeeManager employee = new EmployeeManager();
				foreach (var emp in employees)
				{
					employee.InsertEmployee(emp);
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