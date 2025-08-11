using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            displayCustomers();
            displayEmployees();
            displayLoans();
        }

        private void displayCustomers()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM customertab", con);
                int count = Convert.ToInt32(comm.ExecuteScalar());
                lblCount1.Text = count > 0 ? count.ToString() : "0";
                con.Close();
            }
        }

        private void displayEmployees()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM emptab", con);
                int count = Convert.ToInt32(comm.ExecuteScalar());
                lblCount2.Text = count > 0 ? count.ToString() : "0";
                con.Close();
            }
        }

        private void displayLoans()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM loantab", con);
                int count = Convert.ToInt32(comm.ExecuteScalar());
                lblCount3.Text = count > 0 ? count.ToString() : "0";
                con.Close();
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Login gonow = new Login();
            gonow.Show();
            this.Hide();
        }
    }
}
