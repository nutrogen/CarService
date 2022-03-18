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
    public partial class Billings : Form
    {
        public Billings()
        {
            InitializeComponent();
            DisplayStock();
            GetCars();
            EmpNameLbl.Text = Login.Username; // 4:18 Username was created as a public static variable. The username appears in the upper right of the billing module.
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nutro\\OneDrive\\문서\\CarServiceDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetCars() // 2:24
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select CNum from CarTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNum", typeof(string));
            dt.Load(rdr);
            CarNumCb.ValueMember = "CNum";
            CarNumCb.DataSource = dt;
            con.Close();
        }

        private void DisplayStock() // 2:15 copied from Stock.cs
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

        private void UpdateQty() // 2:54. Updates the part quantity as a part sells.
        {
            int newQty = Qty - Convert.ToInt32(QtyTb.Text); // 2:58 format error. See how to fix it.
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update StockTbl set PartQty=@PQ where PartId=@PId", con);
                cmd.Parameters.AddWithValue("@PId", key);
                cmd.Parameters.AddWithValue("@PQ", newQty);
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int n = 0,num;
        int tot = 0,GrossTot=0;
        private void AddBtn_Click(object sender, EventArgs e) // 2:27
        {
            if(key == 0 || QtyTb.Text == "Quantity" || QtyTb.Text == "" || QtyTb.Text == "")
            {
                MessageBox.Show("Select a part to put in the stock.");
            }
            else if(Convert.ToInt32(QtyTb.Text)>Qty) // 3:00 Added to show "out of stock"
            {
                MessageBox.Show("Out of stock!");
            }
            else
            {
                num = Convert.ToInt32(QtyTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ChangedPartDGV);
                newRow.Cells[0].Value = n + 1; // 2:39. Index out range error. See how to fix it.
                newRow.Cells[1].Value = PartName;
                tot = num * Price;
                newRow.Cells[2].Value = num;
                newRow.Cells[3].Value = Price;
                newRow.Cells[4].Value = tot;
                ChangedPartDGV.Rows.Add(newRow);
                n++; // 2:42 increment of row number
                GrossTot = GrossTot + tot; // Accumulates the gross total. Important!
                PartFeeLbl.Text = "$" + GrossTot;
                UpdateQty();
                QtyTb.Text = "";
            }
        }

        int Tf = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if(MfeesTb.Text == "Service Fee" || MfeesTb.Text == "")
            {
                MessageBox.Show("Enter a valid amount.");
            }
            else if(PartFeeLbl.Text == "Part Fees")
            {
                Tf = Convert.ToInt32(MfeesTb.Text);
                TotFeesLbl.Text = "$"+ Convert.ToString(MfeesTb.Text);
            } else
            {
                Tf = GrossTot + Convert.ToInt32(MfeesTb.Text);
                TotFeesLbl.Text = "$" + Convert.ToString(GrossTot + Convert.ToInt32(MfeesTb.Text)); // Adds the gross total and service fee
            }
        }

        int Qty = 0, Price = 0, key = 0;

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

        private void button1_Click(object sender, EventArgs e) // 3:02 copied from employee.cs
        {
            if (CarNumCb.SelectedIndex == -1 || TotFeesLbl.Text == "Total Fees")
            {
                MessageBox.Show("Data missing!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(CarNum,BDate,MechFees,PartFees,TotFees,EmpName)values(@CN,@BD,@MF,@PF,@TF,@EN)", con);
                    cmd.Parameters.AddWithValue("@CN", CarNumCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BD", BDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MF", MfeesTb.Text);
                    cmd.Parameters.AddWithValue("@PF", GrossTot); // 3:14 serious error. PartFees was substituted as GrossTot
                    cmd.Parameters.AddWithValue("@TF", Tf); // 3:13 changed
                    cmd.Parameters.AddWithValue("@EN", EmpNameLbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill saved!");
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        string PartName = "";
        private void PartsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) // 2:30
        {
            PartName = PartsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Qty = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[2].Value.ToString());
            Price = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[3].Value.ToString());

            if (PartName == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
