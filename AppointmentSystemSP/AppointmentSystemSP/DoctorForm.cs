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
            // Grid initilazition
            DataTable table = dbManager.ExecuteQuery("sp_GetAllDoctors");
            table.Columns["DoctorID"].ColumnName = "No";
            table.Columns["FirstName"].ColumnName = "Ad";
            table.Columns["LastName"].ColumnName = "Soyad";
            table.Columns["PhoneNumber"].ColumnName = "Telefon";
            table.Columns["Email"].ColumnName = "Email";
            table.Columns["Branch"].ColumnName = "Branş";

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
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", txtFirstName.Text),
                new SqlParameter("@LastName", txtLastName.Text),
                new SqlParameter("@PhoneNumber", txtPhone.Text),
                new SqlParameter("@Email", txtEmail.Text),
                new SqlParameter("@Branch", txtBranch.Text)
            };
            dbManager.ExecuteCommand("sp_InsertDoctor", parameters);
            ClearAndNotify(Messages.Insert);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@DoctorID", txtNo.Text),
            };
            dbManager.ExecuteCommand("sp_DeleteDoctor", parameters);
            ClearAndNotify(Messages.Delete);
            UpdateStatus(ProgramStatus.Insert);
        }

        private void btnCreate_Click_1(object sender, EventArgs e)
        {

        }
    }
}
