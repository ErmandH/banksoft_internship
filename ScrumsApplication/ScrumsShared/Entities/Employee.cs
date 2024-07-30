using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumsShared.Entities
{
	public class Employee
	{
		public int EmployeeID { get; set; }
		public string EmployeeCode { get; set; }
		public string EmployeeName { get; set; }
		public string EmployeeSurname { get; set; }

		public static Employee CreateEmployeeFromDataRow(DataRow row)
		{
			Employee employee = new Employee();
			employee.EmployeeID = row.Field<int>("EmployeeID");
			employee.EmployeeCode = row.Field<string>("EmployeeCode");
			employee.EmployeeName = row.Field<string>("EmployeeName");
			employee.EmployeeSurname = row.Field<string>("EmployeeSurname");
			return employee;
		}

		public static IList<Employee> CreateEmployeesFromDataTable(DataTable table)
		{
			IList<Employee> employees = new List<Employee>();
			foreach (DataRow row in table.Rows)
			{
				employees.Add(Employee.CreateEmployeeFromDataRow(row));
			}
			return employees;
		}
	}

}
