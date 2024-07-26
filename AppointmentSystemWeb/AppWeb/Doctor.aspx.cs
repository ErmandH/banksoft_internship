using AppointmentLib.Enums;
using AppointmentLib.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppointSystemWEB
{
    public partial class Doctor : System.Web.UI.Page
    {
        private readonly DbManager dbManager;

        public Doctor()
        {
            dbManager = new DbManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridView();
            }
            UpdateStatus(ProgramStatus.Insert);
        }


        private void UpdateStatus(ProgramStatus newStatus)
        {
            switch (newStatus)
            {
                case ProgramStatus.Insert:
                    btnOlustur.Enabled = true;

                    divNo.Visible = false;
                    btnGuncelle.Enabled = false;
                    btnSil.Enabled = false;
                    break;
                case ProgramStatus.Update:
                    btnOlustur.Enabled = false;

                    divNo.Visible = true;
                    btnGuncelle.Enabled = true;
                    btnSil.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void ClearInputs()
        {
            txtNo.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtBranch.Text = "";
        }

        private void ClearAndNotify()
        {
            ClearInputs();
            LoadGridView();
        }

        protected void btnOlustur_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", txtFirstName.Text),
                new SqlParameter("@LastName", txtLastName.Text),
                new SqlParameter("@PhoneNumber", txtPhone.Text),
                new SqlParameter("@Email", txtEmail.Text),
                new SqlParameter("@Branch", txtBranch.Text)
            };
            dbManager.ExecuteCommand("sp_InsertDoctor", parameters);
            ClearAndNotify();
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@DoctorID", txtNo.Text),
                new SqlParameter("@FirstName", txtFirstName.Text),
                new SqlParameter("@LastName", txtLastName.Text),
                new SqlParameter("@PhoneNumber", txtPhone.Text),
                new SqlParameter("@Email", txtEmail.Text),
                new SqlParameter("@Branch", txtBranch.Text)
            };
            dbManager.ExecuteCommand("sp_UpdateDoctor", parameters);
            ClearAndNotify();
            UpdateStatus(ProgramStatus.Insert);
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@DoctorID", txtNo.Text),
            };
            dbManager.ExecuteCommand("sp_DeleteDoctor", parameters);
            ClearAndNotify();
            UpdateStatus(ProgramStatus.Insert);
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            ClearInputs();
            UpdateStatus(ProgramStatus.Insert);
        }

        private void LoadGridView()
        {
            // Grid initilazition
            DataTable table = dbManager.ExecuteQuery("sp_GetAllDoctors");
            table.Columns["DoctorID"].ColumnName = "No";
            table.Columns["FirstName"].ColumnName = "Ad";
            table.Columns["LastName"].ColumnName = "Soyad";
            table.Columns["PhoneNumber"].ColumnName = "Telefon";
            table.Columns["Email"].ColumnName = "Email";
            table.Columns["Branch"].ColumnName = "Branş";

            gridDoctors.DataSource = table;
            gridDoctors.DataBind();
        }

        protected void gridDoctors_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen satırın indeksini alın
            int selectedIndex = gridDoctors.SelectedIndex;
            if (selectedIndex >= 0)
            {
                // Seçilen satırın verilerini alın
                GridViewRow row = gridDoctors.Rows[selectedIndex];
                // Örnek olarak, satırın her bir hücresindeki veriyi alabiliriz
                txtNo.Text = row.Cells[1].Text;
                txtFirstName.Text = row.Cells[2].Text;
                txtLastName.Text = row.Cells[3].Text;
                txtPhone.Text = row.Cells[4].Text;
                txtEmail.Text = row.Cells[5].Text;
                txtBranch.Text = row.Cells[6].Text;
                UpdateStatus(ProgramStatus.Update);
            }
        }
    }
}