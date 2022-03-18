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

namespace CarService
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nutro\\OneDrive\\문서\\CarServiceDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string Username = ""; // 4:18 added
        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogincs obj = new AdminLogincs();
            obj.Show();
            this.Hide();
        }

        private void LoginBtn_Click(object sender, EventArgs e)//4:15
        {
            if(UnameTb.Text == "" || PasswrdTb.Text == "")
            {
                MessageBox.Show("Enter user name and password");
            }
            else
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmployeeTbl where EmpName='"+UnameTb.Text+"' and EmpPhone= '"+PasswrdTb.Text+"'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1") // Id: Dog  password: 9857
                {
                    Username = UnameTb.Text;
                    Dashboard obj = new Dashboard();
                    obj.Show();
                    this.Hide();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Wrong user name or password!");
                }
                con.Close();
            }
        }
    }
}
