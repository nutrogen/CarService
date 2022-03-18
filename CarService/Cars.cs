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

namespace CarService
{
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
            DisplayCars();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nutro\\OneDrive\\문서\\CarServiceDb.mdf;Integrated Security=True;Connect Timeout=30");
        // 영상에서는 SqlConnection con = new SqlConnection(@"Data Source=(........) 처럼 @을 붙이지만 본 시스템에서는 @ 안 붙이고뒤에 에러 뜨는 부분에 대하여 "\"를 하나씩 추가한다. 

        private void DisplayCars() // 1:00
        {
            con.Open();
            String query = "select * from CarTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void AddBtn_Click(object sender, EventArgs e) // 48:00
        {
            if(CarNumTb.Text == "Car Number" || CarBrandTb.Text == "Car Brand" || CarModelTb.Text == "Car Model" || ColorTb.Text == "Color" || OwnerNameTb.Text == "Owner Name" || CarNumTb.Text =="" 
              || CarBrandTb.Text == "" || CarModelTb.Text == "" ||OwnerNameTb.Text == "" || ColorTb.Text == "")
            {
                MessageBox.Show("Wrong input");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CarTbl(CNum,CBrand,CModel,CDate,CColor,OwnerName)values(@CN,@CB,@CM,@CD,@CC,@ON)",con);
                    cmd.Parameters.AddWithValue("@CN",CarNumTb.Text);
                    cmd.Parameters.AddWithValue("@CB", CarBrandTb.Text);
                    cmd.Parameters.AddWithValue("@CM", CarModelTb.Text);
                    cmd.Parameters.AddWithValue("@CD",CDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CC", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@ON", OwnerNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle has been registered!");
                    con.Close();
                    DisplayCars();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        string CarKey = "";
        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) // 1:12
        {
            CarKey = CarDGV.SelectedRows[0].Cells[0].Value.ToString(); // 1:22 Added to fix the error at 1:12 below.
            CarNumTb.Text = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            CarBrandTb.Text = CarDGV.SelectedRows[0].Cells[1].Value.ToString();
            CarModelTb.Text = CarDGV.SelectedRows[0].Cells[2].Value.ToString();
            CDate.Text = CarDGV.SelectedRows[0].Cells[3].Value.ToString();
            ColorTb.Text = CarDGV.SelectedRows[0].Cells[4].Value.ToString(); // error at 1:12. see how to fix it.
            OwnerNameTb.Text = CarDGV.SelectedRows[0].Cells[5].Value.ToString();
            /*if(CarNumTb.Text =="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CarDGV.SelectedRows[0].Cells[1].Value.ToString());
            }*/
        }

        private void DeleteBtn_Click(object sender, EventArgs e) // 1:15
        {
            if(CarKey == "")
            {
                MessageBox.Show("Select a car to delete.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CarTbl where CNum=@CN",con); // 1:20 an error occurred where deleting process didn't work.
                    cmd.Parameters.AddWithValue("@CN",CarKey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car deleted!");
                    con.Close();
                    DisplayCars();
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e) // 1:22
        {
            if (CarNumTb.Text == "Car Number" || CarBrandTb.Text == "Car Brand" || CarModelTb.Text == "Car Model" || ColorTb.Text == "Color" || OwnerNameTb.Text == "Owner Name" || CarNumTb.Text == ""
             || CarBrandTb.Text == "" || CarModelTb.Text == "" || OwnerNameTb.Text == "" || ColorTb.Text == "")
            {
                MessageBox.Show("Select a car to update");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CarTbl set CBrand=@CB,CModel=@CM,CDate=@CD,CColor=@CC,OwnerName=@ON where CNum=@CN", con);
                    cmd.Parameters.AddWithValue("@CN", CarKey);
                    cmd.Parameters.AddWithValue("@CB", CarBrandTb.Text);
                    cmd.Parameters.AddWithValue("@CM", CarModelTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CC", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@ON", OwnerNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle's data has been updated!");
                    con.Close();
                    DisplayCars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Stocks obj = new Stocks();
            obj.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee();
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
