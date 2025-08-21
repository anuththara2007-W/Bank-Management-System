using BankApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private int selectedAccId = -1;
        private decimal currentBal = 0m;
        private int custId;
        private string custName;

        // Add event for balance updates
        public delegate void BalanceUpdatedEventHandler(int accountId, decimal newBalance);
        public event BalanceUpdatedEventHandler BalanceUpdated;

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        public DepositWithdraw(int customerID, string customerName) : this()
        {
            custId = customerID;
            custName = customerName;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadCustomerAccount();
        }

        private void LoadCustomerAccount()
        {
            try
            {
                int customerIdToUse = GetCustomerID();

                // Debug logging
                System.Diagnostics.Debug.WriteLine($"Customer ID to use: {customerIdToUse}");

                if (customerIdToUse == 0)
                {
                    MessageBox.Show("Customer ID not found. Please login again.");
                    this.Close();
                    return;
                }

                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();

                    // First, let's see all accounts for this customer
                    string debugQuery = "SELECT Account_ID, Balance, Account_Type FROM accounts WHERE Customer_ID = @cid";
                    SqlCommand debugCmd = new SqlCommand(debugQuery, con);
                    debugCmd.Parameters.AddWithValue("@cid", customerIdToUse);

                    SqlDataReader debugReader = debugCmd.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine($"Accounts for Customer {customerIdToUse}:");
                    while (debugReader.Read())
                    {
                        System.Diagnostics.Debug.WriteLine($"Account ID: {debugReader["Account_ID"]}, Balance: {debugReader["Balance"]}, Type: {debugReader["Account_Type"]}");
                    }
                    debugReader.Close();

                    // Now get the first account (or you can let user select)
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance, Account_Type FROM accounts WHERE Customer_ID = @cid ORDER BY Account_ID", con);
                    cmd.Parameters.AddWithValue("@cid", customerIdToUse);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        selectedAccId = Convert.ToInt32(reader["Account_ID"]);
                        currentBal = Convert.ToDecimal(reader["Balance"]);
                        string accountType = reader["Account_Type"].ToString();

                        // Update the label with more information
                        lblBalance.Text = $"Account: {selectedAccId} ({accountType})\nBalance: ${currentBal:F2}";
                        custId = customerIdToUse;

                        // Debug logging
                        System.Diagnostics.Debug.WriteLine($"Selected Account ID: {selectedAccId}, Balance: {currentBal}");
                    }
                    else
                    {
                        MessageBox.Show($"No account found for Customer ID: {customerIdToUse}\n\nPlease create an account first.");
                        this.Close();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account: {ex.Message}\n\nDetails: {ex.StackTrace}");
                System.Diagnostics.Debug.WriteLine($"LoadCustomerAccount Error: {ex}");
                this.Close();
            }
        }

        private int GetCustomerID()
        {
            // Method 1: Use constructor parameter if available
            if (custId > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Using constructor custId: {custId}");
                return custId;
            }

            // Method 2: Try to get from Session class if it exists
            try
            {
                var sessionType = Type.GetType("Bank__Management_System.Session") ??
                                 Type.GetType("BankApp.Session");
                if (sessionType != null)
                {
                    // Try to get as property first
                    var customerIdProperty = sessionType.GetProperty("CustomerID");
                    if (customerIdProperty != null)
                    {
                        object value = customerIdProperty.GetValue(null);
                        if (value != null && int.TryParse(value.ToString(), out int sessionCustomerId))
                        {
                            System.Diagnostics.Debug.WriteLine($"Using Session property CustomerID: {sessionCustomerId}");
                            return sessionCustomerId;
                        }
                    }

                    // If not found as property, try as field
                    var customerIdField = sessionType.GetField("CustomerID");
                    if (customerIdField != null)
                    {
                        object value = customerIdField.GetValue(null);
                        if (value != null && int.TryParse(value.ToString(), out int sessionCustomerId))
                        {
                            System.Diagnostics.Debug.WriteLine($"Using Session field CustomerID: {sessionCustomerId}");
                            return sessionCustomerId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Session access error: {ex.Message}");
            }

            System.Diagnostics.Debug.WriteLine("No Customer ID found!");
            return 0;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Deposit clicked. selectedAccId: {selectedAccId}, currentBal: {currentBal}");

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.");
                txtAmount.Focus();
                return;
            }

            if (selectedAccId == -1)
            {
                MessageBox.Show("No account selected. Please ensure an account exists for this customer.");
                // Try to reload the account
                LoadCustomerAccount();
                return;
            }

            try
            {
                decimal newBal = currentBal + amount;
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();

                    // Use a transaction for safety
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con, transaction);
                        cmd.Parameters.AddWithValue("@bal", newBal);
                        cmd.Parameters.AddWithValue("@aid", selectedAccId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            // Log the transaction
                            SqlCommand logCmd = new SqlCommand(
                                "INSERT INTO transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) VALUES (@aid, 'Deposit', @amount, GETDATE())",
                                con, transaction);
                            logCmd.Parameters.AddWithValue("@aid", selectedAccId);
                            logCmd.Parameters.AddWithValue("@amount", amount);

                            // Try to log but don't fail if transactions table doesn't exist
                            try { logCmd.ExecuteNonQuery(); } catch { }

                            transaction.Commit();

                            currentBal = newBal;
                            lblBalance.Text = $"Account: {selectedAccId}\nBalance: ${currentBal:F2}";
                            txtAmount.Clear();

                            // Raise the event
                            BalanceUpdated?.Invoke(selectedAccId, newBal);

                            // Also try to refresh any open forms
                            RefreshAllAccountGrids();

                            MessageBox.Show($"Deposit successful!\nNew Balance: ${newBal:F2}");
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Deposit failed. Account not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during deposit: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Deposit Error: {ex}");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Withdraw clicked. selectedAccId: {selectedAccId}, currentBal: {currentBal}");

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.");
                txtAmount.Focus();
                return;
            }

            if (selectedAccId == -1)
            {
                MessageBox.Show("No account selected. Please ensure an account exists for this customer.");
                // Try to reload the account
                LoadCustomerAccount();
                return;
            }

            if (amount > currentBal)
            {
                MessageBox.Show($"Insufficient balance.\nCurrent Balance: ${currentBal:F2}\nRequested Amount: ${amount:F2}");
                return;
            }

            try
            {
                decimal newBal = currentBal - amount;
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();

                    // Use a transaction for safety
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con, transaction);
                        cmd.Parameters.AddWithValue("@bal", newBal);
                        cmd.Parameters.AddWithValue("@aid", selectedAccId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            // Log the transaction
                            