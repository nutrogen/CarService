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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            DisplayEmp();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nutro\\OneDrive\\문서\\CarServiceDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayEmp() // 1:33 copied from Car.cs
        {
            con.Open();
            String query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "Employee Name" || EmpAddTb.Text == "Employee Address" || EmpPhoneTb.Text == "Employee Phone" || EmpGenCb.SelectedIndex == -1 || EmpPhoneTb.Text == ""|| EmpNameTb.Text == "" || EmpAddTb.Text == "")
            {
                MessageBox.Show("Wrong input");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EmpName,EmpGen,EmpAdd,EmpPhone)values(@EN,@EG,@EA,@EP)", con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EG", EmpGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New employee has been registered!");
                    con.Close();
                    DisplayEmp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) // 1:40 copied from Car.cs
        {
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
    
            if(EmpNameTb.Text =="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select employee to delete.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EmpId=@EId", con); // there is no EId
                    cmd.Parameters.AddWithValue("@EId", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee's data deleted!");
                    con.Close();
                    DisplayEmp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "Employee Name" || EmpAddTb.Text == "Employee Address" || EmpPhoneTb.Text == "Employee Phone" || EmpGenCb.SelectedIndex == -1 || EmpPhoneTb.Text == "" || EmpNameTb.Text == "" || EmpAddTb.Text == "")
            {
                MessageBox.Show("Select employee to update");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update EmployeeTbl set EmpName=@EN,EmpGen=@EGen,EmpAdd=@EA,EmpPhone=@EP where EmpId=@EId", con);
                    cmd.Parameters.AddWithValue("@EId", key);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EGen", EmpGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee's data updated!");
                    con.Close();
                    DisplayEmp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cars obj = new Cars();
            obj.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Stocks obj = new Stocks();
            obj.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }
    }
}
