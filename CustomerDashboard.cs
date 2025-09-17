using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BankApp
{
    public partial class CustomerDashboard : Form
    {
        private readonly string connString =
            @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public CustomerDashboard()
        {
            InitializeComponent();
            this.Load += CustomerDashboard_Load; // only once here
        }

        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = "Welcome, " + Session.CustomerName;
            LoadBalance();
            LoadRecentTransactions();
            LoadLoanSummary();
            dgvLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridStyle.ModernizeGrid(dgvLoans);
            dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridStyle.ModernizeGrid(dgvTransactions);


            lblBalance.BackColor = Color.Transparent;
            lblBalance.BorderStyle = BorderStyle.None;
            lblCustomerName.BorderStyle = BorderStyle.None;
            lblCustomerName.BackColor = Color.Transparent;
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
            if (Session.CustomerID <= 0)
            {
                dgvLoans.DataSource = null;
                return;
            }

            string connString = null;
            using (SqlConnection con = new SqlConnection(connString))
            using (SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT 
                     RequestID,
                     LoanType,
                     Amount,
                     Status,
                     RequestDate
                  FROM LoanRequests
                  WHERE Customer_ID = @cid
                  ORDER BY RequestDate DESC", con))
            {
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                try
                {
                    // Load data into the grid
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Show data in the grid
                    dgvLoans.DataSource = dt;
                    dgvLoans.ReadOnly = true;
                    dgvLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Format Amount and RequestDate columns if they exist
                    if (dgvLoans.Columns["Amount"] != null)
                        dgvLoans.Columns["Amount"].DefaultCellStyle.Format = "N2";

                    if (dgvLoans.Columns["RequestDate"] != null)
                        dgvLoans.Columns["RequestDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading loan requests: " + ex.Message);
                }
            }
        }



        // Navigation buttons
        private void btnLoanRequest_Click(object sender, EventArgs e)
        {
            LoanRequest frm = new LoanRequest();
            frm.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Profile frm = new Profile();
            frm.Show();
            this.Hide();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword();
            frm.Show();
            this.Hide();
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            Support frm = new Support();
            frm.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.Show();
            this.Hide();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            TransferFunds trs = new TransferFunds();
            trs.Show();
            this.Hide();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            DepositWithdraw deposit = new DepositWithdraw(Session.CustomerID, "deposit");
            deposit.Show();
            this.Hide();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            DepositWithdraw withdraw = new DepositWithdraw(Session.CustomerID, "withdraw");
            withdraw.Show();
            this.Hide();
        }
    }
}
