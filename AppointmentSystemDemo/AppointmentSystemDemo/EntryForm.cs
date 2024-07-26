using AppointmentSystemDemo.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentSystemDemo
{
    public partial class EntryForm : Form
    {
        public EntryForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppointmentForm afrm = new AppointmentForm();
            afrm.Show();
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            PatientForm pfrm = new PatientForm();
            pfrm.Show();
        }
        
        private void EntryForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoctorForm dfrm = new DoctorForm();
            dfrm.Show();
        }
    }
}
