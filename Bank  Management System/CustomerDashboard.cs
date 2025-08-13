using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class CustomerDashboard : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public CustomerDashboard()
        {
            InitializeComponent();
        }

        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = Session.CustomerName;
            LoadBalance();
            LoadTransactions();
            LoadLoans();
        }

        private void LoadBalance()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(Balance) FROM Accounts WHERE Customer_ID=@cid", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                var result = cmd.ExecuteScalar();
                lblBalance.Text = result != DBNull.Value ? $"${result}" : "$0";
            }
        }

        private void LoadTransactions()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT TOP 5 TID, Transaction_Type, Amount, Transaction_Date 
                                 FROM Transactions 
                                 WHERE Account_ID IN (SELECT Account_ID FROM Accounts WHERE Customer_ID=@cid)
                                 ORDER BY Transaction_Date DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTransactions.DataSource = dt;
            }
        }

        private void LoadLoans()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT Loan_ID, Loan_Type, Amount, Status 
                                 FROM Loans 
                                 WHERE Customer_ID=@cid";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLoans.DataSource = dt;
            }
        }
    }
}
