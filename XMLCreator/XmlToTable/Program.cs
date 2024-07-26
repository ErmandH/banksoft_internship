using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLShared;

namespace XmlToTable
{
    class Program
    {
        static void Main(string[] args)
        {
            string tableName = "Doctors";
            string fileName = tableName + ".xml";
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string xmlFilePath = Path.Combine(projectDirectory, String.Format("XmlFiles/{0}", fileName));
            string spName = "sp_InsertDoctor";

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlFilePath);

            DbManager dbManager = new DbManager();
            DataTable dataTable = dataSet.Tables[tableName];
            foreach (DataRow row in dataTable.Rows)
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@FirstName", row["FirstName"] ),
                    new SqlParameter("@LastName", row["LastName"] ),
                    new SqlParameter("@PhoneNumber", row["PhoneNumber"] ),
                    new SqlParameter("@Email", row["Email"] ),
                    new SqlParameter("@Branch", row["Branch"] ),
                };
                dbManager.ExecuteCommand(spName, parameters);
            }
            Console.WriteLine("Veriler başarıyla veritabanına eklendi.");
            Console.ReadKey();
        }
    }
}