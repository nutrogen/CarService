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
    public partial class Dashboard : Form
    {

        public Dashboard()
        {
            InitializeComponent();
            CountCars();
            CountEmployee();
            CountStock();
            SumAmount();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nutro\\OneDrive\\문서\\CarServiceDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void CountCars()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from CarTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CarLbl.Text = dt.Rows[0][0].ToString();
        }

        private void CountStock()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from StockTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StockLbl.Text = dt.Rows[0][0].ToString();
        }

        private void CountEmployee()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmployeeTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            EmpLbl.Text = dt.Rows[0][0].ToString();
        }

        private void SumAmount()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(TotFees) from BillTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AmountLbl.Text = dt.Rows[0][0].ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
