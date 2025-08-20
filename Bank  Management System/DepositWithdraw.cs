using BankApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private int selectedAccountId = -1;
        private decimal currentBalance = 0m;
        private int customerID;
        private string customerName;

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        public DepositWithdraw(int customerID, string customerName) : this() // Call default constructor
        {
            this.customerID = customerID;
            this.customerName = customerName;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccountData();
        }

        private void LoadAccountData()
        {
            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con);
                    cmd.Parameters.AddWithValue("@cid", customerID); // Use the instance variable
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            selectedAccountId = Convert.ToInt32(reader["Account_ID"]);
                            currentBalance = Convert.ToDecimal(reader["Balance"]);
                            
                            if (lblBalance != null)
                                lblBalance.Text = $"Balance: {currentBalance:C}";
                            
                            // Update form title with customer info
                            this.Text = $"Deposit/Withdraw - {customerName}";
                        }
                        else
                        {
                            MessageBox.Show("No account found for this customer.");
                            this.Close(); // Close form if no account
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account: {ex.Message}");
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            ProcessTransaction(isDeposit: true);
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            ProcessTransaction(isDeposit: false);
        }

        private void ProcessTransaction(bool isDeposit)
        {
            if (selectedAccountId == -1)
            {
                MessageBox.Show("No account selected.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.");
                return;
            }

            try
            {
                decimal newBalance;
                string transactionType;

                if (isDeposit)
                {
                    newBalance = currentBalance + amount;
                    transactionType = "Deposit";
                }
                else
                {
                    if (amount > currentBalance)
                    {
                        MessageBox.Show("Insufficient funds for withdrawal.");
                        return;
                    }
                    newBalance = currentBalance - amount;
                    transactionType = "Withdrawal";
                }

                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    
                    // Start transaction for data consistency
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            // Update account balance
                            SqlCommand updateCmd = new SqlCommand(
                                "UPDATE accounts SET Balance = @bal WHERE Account_ID = @aid", 
                                con, transaction);
                            updateCmd.Parameters.AddWithValue("@bal", newBalance);
                            updateCmd.Parameters.AddWithValue("@aid", selectedAccountId);
                            updateCmd.ExecuteNonQuery();

                            // Record transaction history
                            SqlCommand historyCmd = new SqlCommand(
                                "INSERT INTO transactions (Account_ID, TransactionType, Amount, BalanceAfter, TransactionDate) " +
                                "VALUES (@aid, @type, @amount, @balanceAfter, GETDATE())", 
                                con, transaction);
                            historyCmd.Parameters.AddWithValue("@aid", selectedAccountId);
                            historyCmd.Parameters.AddWithValue("@type", transactionType);
                            historyCmd.Parameters.AddWithValue("@amount", amount);
                            historyCmd.Parameters.AddWithValue("@balanceAfter", newBalance);
                            historyCmd.ExecuteNonQuery();

                            transaction.Commit();

                            currentBalance = newBalance;
                            lblBalance.Text = $"Balance: {currentBalance:C}";
                            txtAmount.Clear();
                            
                            MessageBox.Show($"{transactionType} successful!");
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing transaction: {ex.Message}");
            }
        }

        // Add this method to handle form closing or additional events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}