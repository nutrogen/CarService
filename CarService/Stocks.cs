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
    public partial class Stocks : Form
    {
        public Stocks()
        {
            InitializeComponent();
            DisplayStock();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nutro\\OneDrive\\문서\\CarServiceDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayStock() // 1:51 copied from Car.cs
        {
            con.Open();
            String query = "select * from StockTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PartsDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void AddBtn_Click(object sender, EventArgs e) // 1:53
        {
            if (PNameTb.Text == "PartName" || PQtyTb.Text == "Quanity" || PPriceTb.Text == "Price" || PNameTb.Text == "" || PQtyTb.Text == "" || PPriceTb.Text == "")
            {
                MessageBox.Show("Please enter part's data.","Input error");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StockTbl(PartName,PartQty,PartPrice)values(@PN,@PQ,@PP)", con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", PQtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PPriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Part has been registered!");
                    con.Close();
                    DisplayStock();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        private void PartsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PNameTb.Text = PartsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PQtyTb.Text = PartsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PPriceTb.Text = PartsDGV.SelectedRows[0].Cells[3].Value.ToString();
            
            if (PNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select part to delete.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from StockTbl where PartId=@PId", con);
                    cmd.Parameters.AddWithValue("@PId", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Part deleted!");
                    con.Close();
                    DisplayStock();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "PartName" || PQtyTb.Text == "Quantity" || PPriceTb.Text == "Price" || PNameTb.Text == "" || PQtyTb.Text == "" || PPriceTb.Text == "")
            {
                MessageBox.Show("Select a part to update");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update StockTbl set PartName=@PN,PartQty=@PQ,PartPrice=@PP where PartId=@PId", con);
                    cmd.Parameters.AddWithValue("@PId", key);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", PQtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PPriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Part's data has been updated!","Input error");
                    con.Close();
                    DisplayStock();
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
