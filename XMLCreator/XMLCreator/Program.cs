using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLShared;

namespace XMLCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            StoredProcedureToXml("Doctors" ,"sp_GetAllDoctors");
            Console.ReadLine();
        }

        static void StoredProcedureToXml(string tableName, string procedure, List<SqlParameter> parameters = null)
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string xmlFilePath = Path.Combine(projectDirectory, String.Format("XmlFiles/{0}", tableName + ".xml"));
            DbManager dbManager = new DbManager();
            DataTable dataTable = dbManager.ExecuteQuery(procedure, parameters);
            dataTable.TableName = tableName;

            dataTable.WriteXml(xmlFilePath);
            Console.WriteLine(String.Format("{0} başarıyla XML dosyasına yazıldı.", procedure));
        }
    }
}
