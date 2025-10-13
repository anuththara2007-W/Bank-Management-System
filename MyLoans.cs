using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class MyLoans : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public MyLoans()
        {
            InitializeComponent();
        }

        private void MyLoans_Load(object sender, EventArgs e)
        {
            LoadLoans();
        }

        private void LoadLoans()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT Loan_ID, Loan_Type, Amount, Status FROM Loans WHERE Customer_ID=@cid";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvLoans.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }
    }
}
