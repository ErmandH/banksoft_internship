using AppointmentSystemDemo.Constants;
using AppointmentSystemDemo.Enums;
using AppointmentSystemDemo.Services;
using AppointmentSystemDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentSystemDemo
{
    public partial class AppointmentForm : Form
    {
        private readonly DbManager dbManager;
        public AppointmentForm()
        {
            InitializeComponent();
            this.dbManager = new DbManager();
        }

        private void UpdateStatus(ProgramStatus newStatus)
        {
            switch (newStatus)
            {
                case ProgramStatus.Insert:
                    btnCreate.Enabled = true;

                    txtNo.Visible = false;
                    lblNo.Visible = false;
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    break;
                case ProgramStatus.Update:
                    btnCreate.Enabled = false;

                    txtNo.Visible = true;
                    lblNo.Visible = true;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
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
            dtApp.ResetText();
            txtNote.Text = "";
        }

        private void LoadAppointmentsGrid()
        {
            // Grid initilazion
            DataTable table = dbManager.ExecuteQuery("sp_GetAllAppointments");

            table.Columns["AppointmentID"].ColumnName = "No";
            table.Columns["AppointmentDateTime"].ColumnName = "Tarih";
            table.Columns["PatientName"].ColumnName = "Hasta";
            table.Columns["DoctorName"].ColumnName = "Doktor";
            table.Columns["Branch"].ColumnName = "Branş";
            table.Columns["Notes"].ColumnName = "Not";

            gridAppointments.DataSource = table;
        }

        private void ClearAndNotify(string msg)
        {
            ClearInputs();
            LoadAppointmentsGrid();
            MessageBox.Show(msg);
        }

        private void InitDoctorsCombobox() 
        {
            cmbDoctor.DisplayMember = "Text";
            cmbDoctor.ValueMember = "Value";

            DataTable doctorsTable = dbManager.ExecuteQuery("sp_GetAllDoctors");

            List<ComboboxItem> items = new List<ComboboxItem>();

            foreach (DataRow row in doctorsTable.Rows)
            {
                string fullName = string.Format("{0} {1}", row["FirstName"], row["LastName"]);
                items.Add(new ComboboxItem { Value = row["DoctorID"].ToString(), Text = fullName });
            }
            cmbDoctor.DataSource = items;
            cmbDoctor.SelectedIndex = -1;
        }

        private void InitPatientsCombobox()
        {
            cmbPatient.DisplayMember = "Text";
            cmbPatient.ValueMember = "Value";

            DataTable patientsTable = dbManager.ExecuteQuery("sp_GetAllPatients");

            List<ComboboxItem> items = new List<ComboboxItem>();

            foreach (DataRow row in patientsTable.Rows)
            {
                string fullName = string.Format("{0} {1}", row["FirstName"], row["LastName"]);
                items.Add(new ComboboxItem { Value = row["PatientID"].ToString(), Text = fullName });
            }
            cmbPatient.DataSource = items;
            cmbPatient.SelectedIndex = -1;
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            InitDoctorsCombobox();
            InitPatientsCombobox();
            LoadAppointmentsGrid();
            UpdateStatus(ProgramStatus.Insert);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PatientID", cmbPatient.SelectedValue),
                new SqlParameter("@DoctorID", cmbDoctor.SelectedValue),
                new SqlParameter("@AppointmentDateTime", dtApp.Value),
                new SqlParameter("@Notes", txtNote.Text)
            };
            dbManager.ExecuteCommand("sp_InsertAppointment", parameters);
            ClearAndNotify(Messages.Insert);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@AppointmentID", txtNo.Text),
                new SqlParameter("@PatientID", cmbPatient.SelectedValue),
                new SqlParameter("@DoctorID", cmbDoctor.SelectedValue),
                new SqlParameter("@AppointmentDateTime", dtApp.Value),
                new SqlParameter("@Notes", txtNote.Text)
            };
            dbManager.ExecuteCommand("sp_UpdateAppointment", parameters);
            ClearAndNotify(Messages.Update);
            UpdateStatus(ProgramStatus.Insert);
        }

        private void gridPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridAppointments.Rows[e.RowIndex];
                txtNo.Text = row.Cells["No"].Value.ToString();

                // Seçilen doktorun comboboxa aktarımı
                List<ComboboxItem> doctors = cmbDoctor.DataSource as List<ComboboxItem>;
                var selectedDoctor = doctors.Where(d => d.Text == row.Cells["Doktor"].Value.ToString()).First();
                cmbDoctor.SelectedItem = selectedDoctor;

                // Seçilen hastanın comboboxa aktarımı
                List<ComboboxItem> patients = cmbPatient.DataSource as List<ComboboxItem>;
                var selectedPatient = patients.Where(d => d.Text == row.Cells["Hasta"].Value.ToString()).First();
                cmbPatient.SelectedItem = selectedPatient;

                dtApp.Value = Convert.ToDateTime(row.Cells["Tarih"].Value.ToString());
                txtNote.Text = row.Cells["Not"].Value.ToString();
                UpdateStatus(ProgramStatus.Update);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            UpdateStatus(ProgramStatus.Insert);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@AppointmentID", txtNo.Text),
            };
            dbManager.ExecuteCommand("sp_DeleteAppointment", parameters);
            ClearAndNotify(Messages.Delete);
            UpdateStatus(ProgramStatus.Insert);
        }
    }
}
