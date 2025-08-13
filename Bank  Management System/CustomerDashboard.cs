using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class CustomerDashboard : Form
    {
        private int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public CustomerDashboard(int custId)
        {
            InitializeComponent();
            customerId = custId;
        }

        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            LoadCustomerInfo();
            LoadRecentTransactions();
            LoadLoans();
        }

        private void LoadCustomerInfo()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                SqlCommand cmdName = new SqlCommand("SELECT Customer_Name FROM Customer WHERE Customer_ID = @cid", con);
                cmdName.Parameters.AddWithValue("@cid", customerId);
                label1.Text = "Welcome, " + (cmdName.ExecuteScalar()?.ToString() ?? "Customer");

                SqlCommand cmdBal = new SqlCommand("SELECT ISNULL(SUM(Balance),0) FROM Accounts WHERE Customer_ID = @cid", con);
                cmdBal.Parameters.AddWithValue("@cid", customerId);
                object bal = cmdBal.ExecuteScalar();
                decimal balance = (bal == DBNull.Value) ? 0m : Convert.ToDecimal(bal);
                lblBalance.Text = balance.ToString("C2");
            }
        }

        private void LoadRecentTransactions()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string q = @"SELECT TOP 8 T.TID, T.Transaction_Type, T.Amount, T.Transaction_Date, T.Account_ID
                             FROM Transactions T
                             JOIN Accounts A ON T.Account_ID = A.Account_ID
                             WHERE A.Customer_ID = @cid
                             ORDER BY T.Transaction_Date DESC";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTransactions.AutoGenerateColumns = true;
                dgvTransactions.DataSource = dt;
            }
        }

        private void LoadLoans()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string q = "SELECT LoanID, LoanType, Amount, InterestRate, LoanDate, Status FROM Loan WHERE Customer_ID = @cid";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLoans.AutoGenerateColumns = true;
                dgvLoans.DataSource = dt;
            }
        }

        // button click handlers open respective forms (see CustomerMain for pattern)
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            new DepositWithdraw(customerId, "Deposit").ShowDialog();
            LoadCustomerInfo(); LoadRecentTransactions();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            new DepositWithdraw(customerId, "Withdraw").ShowDialog();
            LoadCustomerInfo(); LoadRecentTransactions();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            new TransferFunds(customerId).ShowDialog();
            LoadCustomerInfo(); LoadRecentTransactions();
        }

        private void btnLoanRequest_Click(object sender, EventArgs e)
        {
            new LoanRequest(customerId).ShowDialog();
            LoadLoans();
        }

        private void btnProfile_Click(object sender, EventArgs e) { new Profile(customerId).ShowDialog(); }
        private void btnChangePassword_Click(object sender, EventArgs e) { new ChangePassword(customerId).ShowDialog(); }
        private void btnSupport_Click(object sender, EventArgs e) { new Support(customerId).ShowDialog(); }
    }
}
