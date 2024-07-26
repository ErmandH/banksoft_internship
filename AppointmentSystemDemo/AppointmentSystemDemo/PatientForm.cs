using AppointmentSystemDemo.Constants;
using AppointmentSystemDemo.Enums;
using AppointmentSystemDemo.Services;
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
    public partial class PatientForm : Form
    {
        private readonly DbManager dbManager;

        public PatientForm()
        {
            InitializeComponent();
            this.dbManager = new DbManager();
        }

        private void label5_Click(object sender, EventArgs e)
        {

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
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            cmbGender.Text = "";
        }

        private void LoadPatientsGrid()
        {
            // Grid initilazion
            DataTable table = new DataTable();
            table.Columns.Add("No", typeof(int)).ReadOnly = true;
            table.Columns.Add("Ad", typeof(string)).ReadOnly = true;
            table.Columns.Add("Soyad", typeof(string)).ReadOnly = true;
            table.Columns.Add("Cinsiyet", typeof(string)).ReadOnly = true;
            table.Columns.Add("Telefon", typeof(string)).ReadOnly = true;

            SqlDataReader patients = dbManager.ExecuteReader("select * from Patients");
            while (patients.Read())
            {
                table.Rows.Add(patients[0], patients[1], patients[2], patients[3], patients[4]);
            }
            patients.Close();
            gridPatients.DataSource = table;
        }

        private void ClearAndNotify(string msg) 
        {
            ClearInputs();
            LoadPatientsGrid();
            MessageBox.Show(msg);
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
            // Update Status to Insert
            UpdateStatus(ProgramStatus.Insert);

            // Combobox initilazion
            cmbGender.Items.Add("ERKEK");
            cmbGender.Items.Add("KADIN");
            LoadPatientsGrid();
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string insertSQL = "insert into Patients (FirstName, LastName, Gender, PhoneNumber) values(@p1,@p2,@p3,@p4)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@p1", txtFirstName.Text),
                new SqlParameter("@p2", txtLastName.Text),
                new SqlParameter("@p3", cmbGender.Text),
                new SqlParameter("@p4", txtPhone.Text)
            };
            dbManager.ExecuteCommand(insertSQL, parameters);
            ClearAndNotify(Messages.Insert);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string updateSQL = "UPDATE Patients SET FirstName = @p1, LastName = @p2, Gender = @p3, PhoneNumber = @p4 WHERE PatientID = @p5";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@p1", txtFirstName.Text),
                new SqlParameter("@p2", txtLastName.Text),
                new SqlParameter("@p3", cmbGender.Text),
                new SqlParameter("@p4", txtPhone.Text),
                new SqlParameter("@p5", txtNo.Text)
            };
            dbManager.ExecuteCommand(updateSQL, parameters);
            ClearAndNotify(Messages.Update);
            UpdateStatus(ProgramStatus.Insert);
        }

        private void gridPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Satırın geçerli bir satır olduğunu kontrol etmek
            {
                DataGridViewRow row = gridPatients.Rows[e.RowIndex];

                // Örnek olarak, satırın her bir hücresindeki veriyi alabiliriz
                txtNo.Text = row.Cells["No"].Value.ToString();
                txtFirstName.Text = row.Cells["Ad"].Value.ToString();
                txtLastName.Text = row.Cells["Soyad"].Value.ToString();
                cmbGender.Text = row.Cells["Cinsiyet"].Value.ToString();
                txtPhone.Text = row.Cells["Telefon"].Value.ToString();

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
