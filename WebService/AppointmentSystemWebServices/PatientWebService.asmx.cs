using AppointmentLib.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AppointmentSystemWebServices
{
    /// <summary>
    /// Summary description for PatientWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PatientWebService : System.Web.Services.WebService
    {
        private readonly DbManager dbManager;
        public PatientWebService()
        {
            this.dbManager = new DbManager();
        }

        [WebMethod]
        public DataTable GetAllPatients()
        {
            var result = dbManager.ExecuteQuery("sp_GetAllPatients");
            result.TableName = "Patients";
            return result;
        }

        [WebMethod]
        public void InsertPatient(string firstName, string lastName, string gender, string phoneNumber)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@Gender", gender),
                new SqlParameter("@PhoneNumber", phoneNumber)
            };
            dbManager.ExecuteCommand("sp_InsertPatient", parameters);
        }

        [WebMethod]
        public void UpdatePatient(int patientId, string firstName, string lastName, string gender, string phoneNumber)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@PatientID", patientId),
            new SqlParameter("@FirstName", firstName),
            new SqlParameter("@LastName", lastName),
            new SqlParameter("@Gender", gender),
            new SqlParameter("@PhoneNumber", phoneNumber)
        };
            dbManager.ExecuteCommand("sp_UpdatePatient", parameters);
        }

        [WebMethod]
        public void DeletePatient(int patientId)
        {
            SqlParameter parameter = new SqlParameter("@PatientID", patientId);
            dbManager.ExecuteCommand("sp_DeletePatient", new List<SqlParameter> { parameter });
        }
    }
}
