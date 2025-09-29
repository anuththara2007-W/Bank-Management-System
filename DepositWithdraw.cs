using BankApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private int selectedAccId = -1;
        private decimal currentBal = 0m;
        private int custId;
        private string custName;
        private string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public DepositWithdraw()
        {
            InitializeComponent();
            this.Load += DepositWithdraw_Load;
        }

        public DepositWithdraw(int customerID, string customerName) : this()
        {
            custId = customerID;
            custName = customerName;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadCustomerAccount();
            LoadAccountsToGrid();

        }

        // Load all accounts into grid
        private void LoadAccountsToGrid()
        {
            if (GetCustomerID() == 0) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT TOP 10 Transaction_Type, Amount, Transaction_Date FROM Transactions WHERE Account_ID IN (SELECT Account_ID FROM Accounts WHERE Customer_ID = @cid) ORDER BY Transaction_Date DESC", con);
                    da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                    DataTable dt = new DataTable();
                    gridAccounts.DataSource = dt;
                    da.Fill(dt);

             
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

        private void LoadCustomerAccount()
        {
            try
            {
                int customerIdToUse = GetCustomerID();
                if (customerIdToUse == 0)
                {
                    MessageBox.Show("Customer ID not found. Please login again.");
                    this.Close();
                    return;
                }

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance, Account_Type FROM Accounts WHERE Customer_ID=@cid ORDER BY Account_ID", con);
                    cmd.Parameters.AddWithValue("@cid", customerIdToUse);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Get account ID from reader
                        selectedAccId = Convert.ToInt32(reader["Account_ID"]);

                        // Get balance from reader
                        currentBal = Convert.ToDecimal(reader["Balance"]);

                        // Get account type as string
                        string accountType = reader["Account_Type"].ToString();

                        // Show account info on label
                        lblBalance.Text = $"Account: {selectedAccId} ({accountType})\nBalance: ${currentBal:F2}";

                        // Save customer ID for later
                        custId = customerIdToUse;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading account: " + ex.Message);
                this.Close();
            }
        }

        private int GetCustomerID()
        {
            if (custId > 0) return custId;
            try { return Session.CustomerID; }
            catch { return 0; }
        }

        // Refresh grid after deposit/withdraw and highlight updated account
        private void RefreshGrid() => LoadAccountsToGrid();

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0) return;
            if (selectedAccId == -1) return;

            try
            {
                decimal newBal = currentBal + amount;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Accounts SET Balance=@bal WHERE Account_ID=@aid", con, tran);
                    cmd.Parameters.AddWithValue("@bal", newBal);
                    cmd.Parameters.AddWithValue("@aid", selectedAccId);
                    cmd.ExecuteNonQuery();

                    SqlCommand logCmd = new SqlCommand(
                        "INSERT INTO Transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) VALUES (@aid,'Deposit',@amt,GETDATE())",
                        con, tran);
                    logCmd.Parameters.AddWithValue("@aid", selectedAccId);
                    logCmd.Parameters.AddWithValue("@amt", amount);
                    logCmd.ExecuteNonQuery();

                    tran.Commit();

                    currentBal = newBal;
                    lblBalance.Text = $"Account: {selectedAccId}\nBalance: ${currentBal:F2}";
                    txtAmount.Clear();

                    RefreshGrid();
                    MessageBox.Show("Deposit successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deposit error: " + ex.Message);
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0) return;
            if (selectedAccId == -1 || amount > currentBal) return;

            try
            {
                decimal newBal = currentBal - amount;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Accounts SET Balance=@bal WHERE Account_ID=@aid", con, tran);
                    cmd.Parameters.AddWithValue("@bal", newBal);
                    cmd.Parameters.AddWithValue("@aid", selectedAccId);
                    cmd.ExecuteNonQuery();

                    SqlCommand logCmd = new SqlCommand(
                        "INSERT INTO Transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) VALUES (@aid,'Withdraw',@amt,GETDATE())",
                        con, tran);
                    logCmd.Parameters.AddWithValue("@aid", selectedAccId);
                    logCmd.Parameters.AddWithValue("@amt", amount);
                    logCmd.ExecuteNonQuery();

                    tran.Commit();

                    currentBal = newBal;
                    lblBalance.Text = $"Account: {selectedAccId}\nBalance: ${currentBal:F2}";
                    txtAmount.Clear();

                    RefreshGrid();
                    MessageBox.Show("Withdrawal successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Withdraw error: " + ex.Message);
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }

        private void DepositWithdraw_Load_1(object sender, EventArgs e)
        {
            gridAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridStyle.ModernizeGrid(gridAccounts);
            lblBalance.BackColor = Color.Transparent;
            lblBalance.BorderStyle = BorderStyle.None;
            lblAmount.BorderStyle = BorderStyle.None;
            lblAmount.BackColor = Color.Transparent;
        }
    }
}
