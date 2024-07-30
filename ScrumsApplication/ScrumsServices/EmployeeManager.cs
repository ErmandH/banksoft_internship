using ScrumsShared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsServices
{
	public class EmployeeManager
	{
		private DbManager dbManager;

		public EmployeeManager()
		{
			dbManager = new DbManager();
		}

		public DataTable GetAllEmployees()
		{
			DataTable table = dbManager.ExecuteQuery("scsp_GetAllEmployees");
			return table;
		}

		public void UpdateEmployee(Employee updatedEmployee)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@EmployeeID", updatedEmployee.EmployeeID),
            new SqlParameter("@EmployeeCode", updatedEmployee.EmployeeCode),
            new SqlParameter("@EmployeeName", updatedEmployee.EmployeeName),
            new SqlParameter("@EmployeeSurname", updatedEmployee.EmployeeSurname)
        };
			dbManager.ExecuteCommand("scsp_UpdateEmployee", parameters);
		}

		public void InsertEmployee(Employee employee)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@EmployeeCode", employee.EmployeeCode),
            new SqlParameter("@EmployeeName", employee.EmployeeName),
            new SqlParameter("@EmployeeSurname", employee.EmployeeSurname)
        };
			dbManager.ExecuteCommand("scsp_InsertEmployee", parameters);
		}

		public void DeleteEmployee(int employeeId)
		{
			List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@EmployeeID", employeeId)
        };
			dbManager.ExecuteCommand("scsp_DeleteEmployee", parameters);
		}
	}

}
