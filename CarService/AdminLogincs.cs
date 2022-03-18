using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService
{
    public partial class AdminLogincs : Form
    {
        public AdminLogincs()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(AdmPasswordTb.Text == "")
            {
                MessageBox.Show("Enter the password");
            }
            else if(AdmPasswordTb.Text == "Password")
            {
                Employee obj = new Employee();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Password is wrong!"); // resume at 4:08
            }
        }
    }
}
