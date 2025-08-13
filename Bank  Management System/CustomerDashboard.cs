using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class CustomerDashboard : Form
    {
        public CustomerDashboard()
        {
            InitializeComponent();
        }

      

        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = Session.CustomerName;
            LoadBalance();
            LoadRecentTransactions();
            LoadLoanSummary();
        }

        private void LoadBalance()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT SUM(Balance) FROM Accounts WHERE Customer_ID = @cid", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                object result = cmd.ExecuteScalar();
                decimal balance = (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
                lblBalance.Text = balance.ToString("C");
            }
        }

        private void LoadRecentTransactions()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT TOP 5 Transaction_Type, Amount, Transaction_Date " +
                    "FROM Transactions WHERE Account_ID IN " +
                    "(SELECT Account_ID FROM Accounts WHERE Customer_ID = @cid) " +
                    "ORDER BY Transaction_Date DESC", con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTransactions.DataSource = dt;
            }
        }

        private void LoadLoanSummary()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT Loan_ID, Loan_Type, Amount, Status FROM Loans " +
                    "WHERE Customer_ID = @cid", con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLoans.DataSource = dt;
            }
        }

        private void btnLoanRequest_Click(object sender, EventArgs e)
        {
            LoanRequest frm = new LoanRequest();
            frm.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Profile frm = new Profile();
            frm.ShowDialog();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword();
            frm.ShowDialog();
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            Support frm = new Support();
            frm.ShowDialog();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.Show();
            this.Close();
        }
}
