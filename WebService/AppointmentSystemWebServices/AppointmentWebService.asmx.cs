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
    /// Summary description for AppointmentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AppointmentWebService : System.Web.Services.WebService
    {

        private readonly DbManager dbManager;
        public AppointmentWebService()
        {
            this.dbManager = new DbManager();
        }

        [WebMethod]
        public DataTable GetAllAppointments()
        {
            var result = dbManager.ExecuteQuery("sp_GetAllAppointments");
            result.TableName = "Appointments";
            return result;
        }

        [WebMethod]
        public void InsertAppointment(int patientId, int doctorId, string appDate, string note)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PatientID", patientId),
                new SqlParameter("@DoctorID", doctorId),
                new SqlParameter("@AppointmentDateTime", Convert.ToDateTime(appDate)),
                new SqlParameter("@Notes", note)
            };
            dbManager.ExecuteCommand("sp_InsertAppointment", parameters);
        }

        [WebMethod]
        public void UpdateAppointment(int appointmentId, int patientId, int doctorId, string appDate, string note)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@AppointmentID", appointmentId),
            new SqlParameter("@PatientID", patientId),
            new SqlParameter("@DoctorID", doctorId),
            new SqlParameter("@AppointmentDateTime", Convert.ToDateTime(appDate)),
            new SqlParameter("@Notes", note)
        };
            dbManager.ExecuteCommand("sp_UpdateAppointment", parameters);
        }

        [WebMethod]
        public void DeleteAppointment(int appointmentId)
        {
            SqlParameter parameter = new SqlParameter("@AppointmentID", appointmentId);
            dbManager.ExecuteCommand("sp_DeleteAppointment", new List<SqlParameter> { parameter });
        }
    }
}
