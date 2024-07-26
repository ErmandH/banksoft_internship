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
    /// Summary description for UserWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserWebService : System.Web.Services.WebService
    {

        private readonly DbManager dbManager;

        public UserWebService()
        {
            this.dbManager = new DbManager();
        }

        [WebMethod]
        public DataTable GetAllUsers()
        {
            var result = dbManager.ExecuteQuery("sp_GetAllUsers");
            result.TableName = "Users";
            return result;
        }

        [WebMethod]
        public void InsertUser(string username, string password)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password)
        };
            dbManager.ExecuteCommand("sp_InsertUser", parameters);
        }

        [WebMethod]
        public void UpdateUser(int userId, string username, string password)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password)
        };
            dbManager.ExecuteCommand("sp_UpdateUser", parameters);
        }

        [WebMethod]
        public void DeleteUser(int userId)
        {
            SqlParameter parameter = new SqlParameter("@UserID", userId);
            dbManager.ExecuteCommand("sp_DeleteUser", new List<SqlParameter> { parameter });
        }

        [WebMethod]
        public DataTable AuthenticateUser(string username, string password)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };
            var result = dbManager.ExecuteQuery("sp_AuthenticateUser", parameters);
            result.TableName = "User";
            return result;
        }

        [WebMethod]
        public DataTable GetUserByUsername(string username)
        {
            SqlParameter parameter = new SqlParameter("@Username", username);
            var result = dbManager.ExecuteQuery("sp_GetUserByUsername", new List<SqlParameter> { parameter });
            result.TableName = "User";
            return result;
        }
    }
}
