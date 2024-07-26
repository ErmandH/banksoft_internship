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
            DataTable table = new DataTable();
            table.Columns.Add("No", typeof(int)).ReadOnly = true;
            table.Columns.Add("Tarih", typeof(DateTime)).ReadOnly = true;
            table.Columns.Add("Hasta", typeof(string)).ReadOnly = true;
            table.Columns.Add("Doktor", typeof(string)).ReadOnly = true;
            table.Columns.Add("Branş", typeof(string)).ReadOnly = true;
            table.Columns.Add("Not", typeof(string)).ReadOnly = true;

            SqlDataReader appointments = dbManager.ExecuteReader("select AppointmentID, AppointmentDateTime, Patients.FirstName + ' ' + Patients.LastName AS PatientName, Doctors.FirstName + ' ' + Doctors.LastName AS DoctorName, Doctors.Branch, Notes from Appointments join Patients on Appointments.PatientID = Patients.PatientID join Doctors on Appointments.DoctorID = Doctors.DoctorID");
            while (appointments.Read())
            {
                table.Rows.Add(appointments[0], appointments[1], appointments[2], appointments[3], appointments[4], appointments[5]);
            }
            appointments.Close();
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

            SqlDataReader doctors = dbManager.ExecuteReader("select * from Doctors");
            List<ComboboxItem> items = new List<ComboboxItem>();
            while (doctors.Read())
            {
                items.Add(new ComboboxItem { Value = doctors[0].ToString(), Text = doctors[1] + " " + doctors[2] });
            }
            cmbDoctor.DataSource = items;
            cmbDoctor.SelectedIndex = -1;
        }

        private void InitPatientsCombobox()
        {
            cmbPatient.DisplayMember = "Text";
            cmbPatient.ValueMember = "Value";

            SqlDataReader patients = dbManager.ExecuteReader("select * from Patients");
            List<ComboboxItem> items = new List<ComboboxItem>();
            while (patients.Read())
            {
                items.Add(new ComboboxItem { Value = patients[0].ToString(), Text = patients[1] + " " + patients[2] });
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
            string insertSQL = "insert into Appointments (PatientID, DoctorID, AppointmentDateTime, Notes) values(@p1,@p2,@p3,@p4)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@p1", cmbPatient.SelectedValue),
                new SqlParameter("@p2", cmbDoctor.SelectedValue),
                new SqlParameter("@p3", dtApp.Value),
                new SqlParameter("@p4", txtNote.Text)
            };
            dbManager.ExecuteCommand(insertSQL, parameters);
            ClearAndNotify(Messages.Insert);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string updateSQL = "UPDATE Appointments SET PatientID = @p1, DoctorID = @p2, AppointmentDateTime = @p3, Notes = @p4 WHERE AppointmentID = @p5";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@p1", cmbPatient.SelectedValue),
                new SqlParameter("@p2", cmbDoctor.SelectedValue),
                new SqlParameter("@p3", dtApp.Value),
                new SqlParameter("@p4", txtNote.Text),
                new SqlParameter("@p5", txtNo.Text)
            };
            dbManager.ExecuteCommand(updateSQL, parameters);
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
            string deleteCmdString = "DELETE FROM Patients WHERE PatientID=" + txtNo.Text;
            dbManager.ExecuteCommand(deleteCmdString);
            ClearAndNotify(Messages.Delete);
            UpdateStatus(ProgramStatus.Insert);
        }
    }
}
