using AppointmentLib.Services;
using AppointmentLib.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppointSystemWEB
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly DbManager dbManager;
        public Login()
        {
            this.dbManager = new DbManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string hashedPassword = Sha256HashGenerator.Generate(txtPassword.Text);
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Username", txtUsername.Text),
                    new SqlParameter("@Password", hashedPassword)
                };
                DataTable result = dbManager.ExecuteQuery("sp_AuthenticateUser", parameters);
                if (result.Rows.Count > 0)
                {
                    FormsAuthentication.SetAuthCookie(txtUsername.Text, false);
                    string redirectUrl = FormsAuthentication.GetRedirectUrl(txtUsername.Text, false);
                    Response.Redirect(redirectUrl);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid username or password');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", ex.Message), true);
            }

        }
    }
}