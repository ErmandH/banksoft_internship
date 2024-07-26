using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentLib.Services
{
    public class DbManager
    {
        private readonly string connectionString;

        public DbManager()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["AppointmentConnString"].ConnectionString;
        }

        public DataTable ExecuteQuery(string procedure, List<SqlParameter> parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(procedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public void ExecuteCommand(string procedure, List<SqlParameter> parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(procedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
