using AppointmentLib.Enums;
using AppointmentLib.Services;
using AppointSystemWEB.ViewModels;
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
    public partial class _Default : Page
    {
        private readonly DbManager dbManager;

        public _Default()
        {
            dbManager = new DbManager();
        }

        private void InitDoctorsCombobox()
        {
            DataTable doctorsTable = dbManager.ExecuteQuery("sp_GetAllDoctors");

            List<ComboBoxItem> items = new List<ComboBoxItem>();

            foreach (DataRow row in doctorsTable.Rows)
            {
                string fullName = string.Format("{0} {1}", row["FirstName"], row["LastName"]);
                items.Add(new ComboBoxItem { Value = row["DoctorID"].ToString(), Text = fullName });
            }
            cmbDoctor.DataSource = items;
            cmbDoctor.DataTextField = "Text";
            cmbDoctor.DataValueField = "Value";
            cmbDoctor.DataBind();
            cmbDoctor.SelectedIndex = -1;
        }

        private void InitPatientsCombobox()
        {


            DataTable patientsTable = dbManager.ExecuteQuery("sp_GetAllPatients");

            List<ComboBoxItem> items = new List<ComboBoxItem>();

            foreach (DataRow row in patientsTable.Rows)
            {
                string fullName = string.Format("{0} {1}", row["FirstName"], row["LastName"]);
                items.Add(new ComboBoxItem { Value = row["PatientID"].ToString(), Text = fullName });
            }
            cmbPatient.DataSource = items;
            cmbPatient.DataValueField = "Value";
            cmbPatient.DataTextField = "Text";
            cmbPatient.DataBind();
            cmbPatient.SelectedIndex = -1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridView();

            }
            UpdateStatus(ProgramStatus.Insert);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            InitDoctorsCombobox();
            InitPatientsCombobox();
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
            cmbDoctor.SelectedIndex = -1;
            cmbPatient.SelectedIndex = -1;
            dtApp.Text = DateTime.Now.ToString();
            txtNote.Text = "";
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
                new SqlParameter("@PatientID", cmbPatient.SelectedValue),
                new SqlParameter("@DoctorID", cmbDoctor.SelectedValue),
                new SqlParameter("@AppointmentDateTime", Convert.ToDateTime(dtApp.Text)),
                new SqlParameter("@Notes", txtNote.Text)
            };
            dbManager.ExecuteCommand("sp_InsertAppointment", parameters);
            ClearAndNotify();
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@AppointmentID", txtNo.Text),
                new SqlParameter("@PatientID", cmbPatient.SelectedValue),
                new SqlParameter("@DoctorID", cmbDoctor.SelectedValue),
                new SqlParameter("@AppointmentDateTime", Convert.ToDateTime(dtApp.Text)),
                new SqlParameter("@Notes", txtNote.Text)
            };
            dbManager.ExecuteCommand("sp_UpdateAppointment", parameters);
            ClearAndNotify();
            UpdateStatus(ProgramStatus.Insert);
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@AppointmentID", txtNo.Text),
            };
            dbManager.ExecuteCommand("sp_DeleteAppointment", parameters);
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
            DataTable table = dbManager.ExecuteQuery("sp_GetAllAppointments");

            table.Columns["AppointmentID"].ColumnName = "No";
            table.Columns["AppointmentDateTime"].ColumnName = "Tarih";
            table.Columns["PatientName"].ColumnName = "Hasta";
            table.Columns["DoctorName"].ColumnName = "Doktor";
            table.Columns["Branch"].ColumnName = "Branş";
            table.Columns["Notes"].ColumnName = "Not";

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
                GridViewRow row = gridPatients.Rows[selectedIndex];

                txtNo.Text = row.Cells[1].Text;

                // Seçilen doktorun comboboxa aktarımı
                List<ComboBoxItem> doctors = cmbDoctor.DataSource as List<ComboBoxItem>;
                var selectedDoctor = doctors.FindIndex(d => d.Text == row.Cells[4].Text);
                cmbDoctor.SelectedIndex = Convert.ToInt16(selectedDoctor);

                // Seçilen hastanın comboboxa aktarımı
                List<ComboBoxItem> patients = cmbPatient.DataSource as List<ComboBoxItem>;
                var selectedPatient = patients.FindIndex(d => d.Text == row.Cells[3].Text);
                cmbPatient.SelectedIndex = Convert.ToInt16(selectedPatient);

                dtApp.Text = DateTime.Parse(row.Cells[2].Text).ToString("yyyy-MM-dd");
                txtNote.Text = row.Cells[6].Text;
                UpdateStatus(ProgramStatus.Update);
            }
        }

    }
}