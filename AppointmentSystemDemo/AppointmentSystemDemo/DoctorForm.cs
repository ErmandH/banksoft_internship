using AppointmentSystemDemo.Services;
using AppointmentSystemDemo.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using AppointmentSystemDemo.Enums;

namespace AppointmentSystemDemo
{
    public partial class DoctorForm : Form
    {
        private readonly DbManager dbManager;
        public DoctorForm()
        {
            InitializeComponent();
            this.dbManager = new DbManager();
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

        private void LoadDoctorsGrid()
        {
            // Grid initilazion
            DataTable table = new DataTable();
            table.Columns.Add("No", typeof(int)).ReadOnly = true;
            table.Columns.Add("Ad", typeof(string)).ReadOnly = true;
            table.Columns.Add("Soyad", typeof(string)).ReadOnly = true;
            table.Columns.Add("Telefon", typeof(string)).ReadOnly = true;
            table.Columns.Add("Email", typeof(string)).ReadOnly = true;
            table.Columns.Add("Branş", typeof(string)).ReadOnly = true;

            SqlDataReader doctors = dbManager.ExecuteReader("select * from Doctors");
            while (doctors.Read())
            {
                table.Rows.Add(doctors[0], doctors[1], doctors[2], doctors[3], doctors[4], doctors[5]);
            }
            doctors.Close();
            gridDoctors.DataSource = table;
        }

        private void ClearAndNotify(string msg)
        {
            ClearInputs();
            LoadDoctorsGrid();
            MessageBox.Show(msg);
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

        private void DoctorForm_Load(object sender, EventArgs e)
        {
            UpdateStatus(ProgramStatus.Insert);
            LoadDoctorsGrid();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string insertSQL = "insert into doctors (FirstName, LastName, PhoneNumber, Email, Branch) values(@p1,@p2,@p3,@p4,@p5)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@p1", txtFirstName.Text),
                new SqlParameter("@p2", txtLastName.Text),
                new SqlParameter("@p3", txtPhone.Text),
                new SqlParameter("@p4", txtEmail.Text),
                new SqlParameter("@p5", txtBranch.Text)
            };
            dbManager.ExecuteCommand(insertSQL, parameters);
            ClearAndNotify(Messages.Insert);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string updateSQL = "UPDATE doctors SET FirstName = @p1, LastName = @p2, PhoneNumber = @p3, Email = @p4, Branch = @p5 WHERE DoctorID = @p6";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@p1", txtFirstName.Text),
                new SqlParameter("@p2", txtLastName.Text),
                new SqlParameter("@p3", txtPhone.Text),
                new SqlParameter("@p4", txtEmail.Text),
                new SqlParameter("@p5", txtBranch.Text),
                new SqlParameter("@p6", txtNo.Text)
            };
            dbManager.ExecuteCommand(updateSQL, parameters);
            ClearAndNotify(Messages.Update);
            UpdateStatus(ProgramStatus.Insert);
        }

        private void gridDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Satırın geçerli bir satır olduğunu kontrol etmek
            {
                DataGridViewRow row = gridDoctors.Rows[e.RowIndex];

                // Örnek olarak, satırın her bir hücresindeki veriyi alabiliriz
                txtNo.Text = row.Cells["No"].Value.ToString();
                txtFirstName.Text = row.Cells["Ad"].Value.ToString();
                txtLastName.Text = row.Cells["Soyad"].Value.ToString();
                txtPhone.Text = row.Cells["Telefon"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtBranch.Text = row.Cells["Branş"].Value.ToString();
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
            string deleteCmdString = "DELETE FROM doctors WHERE DoctorID=" + txtNo.Text;
            dbManager.ExecuteCommand(deleteCmdString);
            ClearAndNotify(Messages.Delete);
            UpdateStatus(ProgramStatus.Insert);
        }

        private void btnCreate_Click_1(object sender, EventArgs e)
        {

        }
    }
}
