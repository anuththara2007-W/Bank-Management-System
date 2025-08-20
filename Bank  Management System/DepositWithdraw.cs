using BankApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        // Private fields with unique names to avoid conflicts
        private int _accountId = -1;
        private decimal _balance = 0m;
        private int _customerId;
        private string _customerName;

        // Default constructor
        public DepositWithdraw()
        {
            InitializeComponent();
        }

        // Parameterized constructor
        public DepositWithdraw(int customerId, string customerName) : this()
        {
            _customerId = customerId;
            _customerName = customerName;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadCustomerAccount();
        }

        private void LoadCustomerAccount()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @customerId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@customerId", _customerId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _accountId = Convert.ToInt32(reader["Account_ID"]);
                                _balance = Convert.ToDecimal(reader["Balance"]);
                                UpdateBalanceDisplay();
                            }
                            else
                            {
                                MessageBox.Show("No account found for this customer.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account information: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void UpdateBalanceDisplay()
        {
            if (lblBalance != null)
            {
                lblBalance.Text = $"Balance: ${_balance:F2}";
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            PerformDeposit();
        }

        private void PerformDeposit()
        {
            if (!ValidateInput(out decimal amount))
                return;

            if (!ValidateAccount())
                return;

            try
            {
                decimal newBalance = _balance + amount;

                if (UpdateAccountBalance(newBalance))
                {
                    _balance = newBalance;
                    UpdateBalanceDisplay();
                    ClearAmountField();
                    MessageBox.Show($"Deposit successful!\nNew balance: ${_balance:F2}", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deposit failed. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during deposit: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            PerformWithdrawal();
        }

        private void PerformWithdrawal()
        {
            if (!ValidateInput(out decimal amount))
                return;

            if (!ValidateAccount())
                return;

            if (!ValidateSufficientFunds(amount))
                return;

            try
            {
                decimal newBalance = _balance - amount;

                if (UpdateAccountBalance(newBalance))
                {
                    _balance = newBalance;
                    UpdateBalanceDisplay();
                    ClearAmountField();
                    MessageBox.Show($"Withdrawal successful!\nNew balance: ${_balance:F2}", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Withdrawal failed. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during withdrawal: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput(out decimal amount)
        {
            if (!decimal.TryParse(txtAmount?.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidateAccount()
        {
            if (_accountId == -1)
            {
                MessageBox.Show("No account selected.", "Account Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidateSufficientFunds(decimal amount)
        {
            if (amount > _balance)
            {
                MessageBox.Show($"Insufficient balance.\nAvailable balance: ${_balance:F2}\nRequested amount: ${amount:F2}",
                    "Insufficient Funds", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool UpdateAccountBalance(decimal newBalance)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE accounts SET Balance = @balance WHERE Account_ID = @accountId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@balance", newBalance);
                        command.Parameters.AddWithValue("@accountId", _accountId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw; // Re-throw to be handled by calling method
            }
        }

        private void ClearAmountField()
        {
            if (txtAmount != null)
            {
                txtAmount.Clear();
                txtAmount.Focus();
            }
        }

        // Optional: Refresh balance from database
        private void RefreshBalanceFromDatabase()
        {
            if (_accountId == -1)
                return;

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Balance FROM accounts WHERE Account_ID = @accountId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountId", _accountId);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            _balance = Convert.ToDecimal(result);
                            UpdateBalanceDisplay();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing balance: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}