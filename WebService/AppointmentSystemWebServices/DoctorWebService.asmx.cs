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
    /// Summary description for DoctorWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DoctorWebService : System.Web.Services.WebService
    {
        private readonly DbManager dbManager;
        public DoctorWebService()
        {
            this.dbManager = new DbManager();
        }

        [WebMethod]
        public DataTable GetAllDoctors()
        {
            var result = dbManager.ExecuteQuery("sp_GetAllDoctors");
            result.TableName = "Doctors";
            return result;
        }

        [WebMethod]
        public void InsertDoctor(string firstName, string lastName, string phoneNumber, string email, string branch)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@PhoneNumber", phoneNumber),
                new SqlParameter("@Email", email),
                new SqlParameter("@Branch", branch)
            };
            dbManager.ExecuteCommand("sp_InsertDoctor", parameters);
        }

        [WebMethod]
        public void UpdateDoctor(int doctorId, string firstName, string lastName, string phoneNumber, string email, string branch)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@DoctorID", doctorId),
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@PhoneNumber", phoneNumber),
                new SqlParameter("@Email", email),
                new SqlParameter("@Branch", branch)
            };
            dbManager.ExecuteCommand("sp_UpdateDoctor", parameters);
        }

        [WebMethod]
        public void DeleteDoctor(int doctorId)
        {
            SqlParameter parameter = new SqlParameter("@DoctorID", doctorId);
            dbManager.ExecuteCommand("sp_DeleteDoctor", new List<SqlParameter> { parameter });
        }
    }
}
