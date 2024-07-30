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
	/// Summary description for DeleteEmployeeHandler
	/// </summary>
	public class DeleteEmployeeHandler : IHttpHandler
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
				dynamic data = JsonConvert.DeserializeObject(jsonString);
				int id = data.EmployeeID;

				EmployeeManager employee = new EmployeeManager();
				employee.DeleteEmployee(id);
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