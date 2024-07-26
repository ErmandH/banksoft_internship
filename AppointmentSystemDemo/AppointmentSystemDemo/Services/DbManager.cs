using AppointmentSystemDemo.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AppointmentSystemDemo.Services
{
    public class DbManager
    {
        private readonly string connectionString;

        public DbManager()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["AppointmentConnString"].ConnectionString;
        }

        public SqlDataReader ExecuteReader(string query, List<SqlParameter> parameters = null)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            if (parameters != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }
            else
            {
                cmd.CommandType = CommandType.Text;
            }
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public void ExecuteCommand(string query, List<SqlParameter> parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
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