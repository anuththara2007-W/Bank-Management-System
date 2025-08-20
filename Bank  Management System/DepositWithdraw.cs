using BankApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        // Required variables
        private int selectedAccountId = -1;
        private decimal accountBalance = 0m;
        private int customerID;
        private string customerName;

        // Default constructor
        public DepositWithdraw()
        {
            InitializeComponent();
        }

        // Parameterized constructor - FIXED
        public DepositWithdraw(int customerID, string customerName) : this()
        {
            this.customerID = customerID;
            this.customerName = customerName;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            try
            {
                // Load first account for logged in customer
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con);

                    // FIXED: Use the instance variable instead of Session.CustomerID
                    cmd.Parameters.AddWithValue("@cid", this.customerID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        selectedAccountId = Convert.ToInt32(reader["Account_ID"]);
                        accountBalance = Convert.ToDecimal(reader["Balance"]);
                        lblBalance.Text = $"Balance: ${accountBalance:F2}";
                    }
                    else
                    {
                        MessageBox.Show("No account found for this customer.");
                        this.Close(); // Close form if no account found
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account: {ex.Message}");
                this.Close();
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than 0.");
                    return;
                }

                // Check for valid account
                if (selectedAccountId == -1)
                {
                    MessageBox.Show("No account selected.");
                    return;
                }

                decimal newBalance = accountBalance + amount;

                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE accounts SET Balance = @bal WHERE Account_ID = @aid", con);
                    cmd.Parameters.AddWithValue("@bal", newBalance);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        accountBalance = newBalance;
                        lblBalance.Text = $"Balance: ${accountBalance:F2}";
                        txtAmount.Clear();
                        MessageBox.Show($"Deposit successful! New balance: ${accountBalance:F2}");
                    }
                    else
                    {
                        MessageBox.Show("Deposit failed. Account not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during deposit: {ex.Message}");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than 0.");
                    return;
                }

                // Check for valid account
                if (selectedAccountId == -1)
                {
                    MessageBox.Show("No account selected.");
                    return;
                }

                // Check sufficient balance
                if (amount > accountBalance)
                {
                    MessageBox.Show($"Insufficient balance. Available balance: ${accountBalance:F2}");
                    return;
                }

                decimal newBalance = accountBalance - amount;

                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE accounts SET Balance = @bal WHERE Account_ID = @aid", con);
                    cmd.Parameters.AddWithValue("@bal", newBalance);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        accountBalance = newBalance;
                        lblBalance.Text = $"Balance: ${accountBalance:F2}";
                        txtAmount.Clear();
                        MessageBox.Show($"Withdrawal successful! New balance: ${accountBalance:F2}");
                    }
                    else
                    {
                        MessageBox.Show("Withdrawal failed. Account not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during withdrawal: {ex.Message}");
            }
        }

        // Optional: Add a method to refresh balance from database
        private void RefreshBalance()
        {
            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT Balance FROM accounts WHERE Account_ID = @aid", con);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        accountBalance = Convert.ToDecimal(result);
                        lblBalance.Text = $"Balance: ${accountBalance:F2}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing balance: {ex.Message}");
            }
        }
    }
}