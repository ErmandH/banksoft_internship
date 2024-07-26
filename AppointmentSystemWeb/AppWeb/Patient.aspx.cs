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
    public partial class Patient : System.Web.UI.Page
    {
        private readonly DbManager dbManager;

        public Patient()
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
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtTelefon.Text = "";
            ddlCinsiyet.SelectedIndex = -1;
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
                new SqlParameter("@FirstName", txtAd.Text),
                new SqlParameter("@LastName", txtSoyad.Text),
                new SqlParameter("@Gender", ddlCinsiyet.SelectedValue),
                new SqlParameter("@PhoneNumber", txtTelefon.Text)
            };
            dbManager.ExecuteCommand("sp_InsertPatient", parameters);
            ClearAndNotify();
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", txtAd.Text),
                new SqlParameter("@LastName", txtSoyad.Text),
                new SqlParameter("@Gender", ddlCinsiyet.SelectedValue),
                new SqlParameter("@PhoneNumber", txtTelefon.Text),
                new SqlParameter("@PatientID", txtNo.Text)
            };
            dbManager.ExecuteCommand("sp_UpdatePatient", parameters);
            ClearAndNotify();
            UpdateStatus(ProgramStatus.Insert);
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PatientID", txtNo.Text),
            };
            dbManager.ExecuteCommand("sp_DeletePatient", parameters);
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
            DataTable table = dbManager.ExecuteQuery("sp_GetAllPatients");
            table.Columns["PatientID"].ColumnName = "No";
            table.Columns["FirstName"].ColumnName = "Ad";
            table.Columns["LastName"].ColumnName = "Soyad";
            table.Columns["Gender"].ColumnName = "Cinsiyet";
            table.Columns["PhoneNumber"].ColumnName = "Telefon";

            gridPatients.DataSource = table;
            gridPatients.DataBind();
        }

        protected void gridPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen satırın indeksini alın
            int selectedIndex = gridPatients.SelectedIndex;
            if (selectedIndex >= 0)
            {
                // Seçilen satırın verilerini alın
                GridViewRow selectedRow = gridPatients.Rows[selectedIndex];
                string no = selectedRow.Cells[1].Text;
                string ad = selectedRow.Cells[2].Text;
                string soyad = selectedRow.Cells[3].Text;
                string cinsiyet = selectedRow.Cells[4].Text;
                string telefon = selectedRow.Cells[5].Text;

                // Verileri form alanlarına doldurun
                txtNo.Text = no;
                txtAd.Text = ad;
                txtSoyad.Text = soyad;
                txtTelefon.Text = telefon;
                ddlCinsiyet.SelectedValue = cinsiyet;
            }
            UpdateStatus(ProgramStatus.Update);
        }
    }
}