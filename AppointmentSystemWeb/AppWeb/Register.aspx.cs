using AppointmentLib.Services;
using AppointmentLib.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppointSystemWEB
{
    public partial class Register : System.Web.UI.Page
    {
        private readonly DbManager dbManager;
        public Register()
        {
            this.dbManager = new DbManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string hashedPassword = Sha256HashGenerator.Generate(txtPassword.Text);
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Username", txtUsername.Text),
                    new SqlParameter("@Password", hashedPassword),
                };
                dbManager.ExecuteCommand("sp_InsertUser", parameters);
                Response.Redirect("Login.aspx");

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", ex.Message), true);
            }
        }
                
    }
}