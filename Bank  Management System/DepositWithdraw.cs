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

            // 🔹 Refresh grids when form opens
            RefreshAllAccountGrids();
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

                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance, Account_Type FROM accounts WHERE Customer_ID = @cid ORDER BY Account_ID", con);
                    cmd.Parameters.AddWithValue("@cid", customerIdToUse);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        selectedAccId = Convert.ToInt32(reader["Account_ID"]);
                        currentBal = Convert.ToDecimal(reader["Balance"]);
                        string accountType = reader["Account_Type"].ToString();

                        lblBalance.Text = $"Account: {selectedAccId} ({accountType})\nBalance: ${currentBal:F2}";
                        custId = customerIdToUse;
                    }
                    else
                    {
                        MessageBox.Show("No account found. Please create one first.");
                        this.Close();
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

        private int GetCustomerID()
        {
            if (custId > 0) return custId;
            try
            {
                return Session.CustomerID; // directly use session
            }
            catch
            {
                return 0;
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount > 0");
                return;
            }

            if (selectedAccId == -1)
            {
                MessageBox.Show("No account selected.");
                return;
            }

            try
            {
                decimal newBal = currentBal + amount;
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
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
                            SqlCommand logCmd = new SqlCommand(
                                "INSERT INTO transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) VALUES (@aid, 'Deposit', @amount, GETDATE())",
                                con, transaction);
                            logCmd.Parameters.AddWithValue("@aid", selectedAccId);
                            logCmd.Parameters.AddWithValue("@amount", amount);
                            try { logCmd.ExecuteNonQuery(); } catch { }

                            transaction.Commit();

                            currentBal = newBal;
                            lblBalance.Text = $"Account: {selectedAccId}\nBalance: ${currentBal:F2}";
                            txtAmount.Clear();

                            // 🔹 Refresh grids after deposit
                            RefreshAllAccountGrids();

                            MessageBox.Show($"Deposit successful! New Balance: ${newBal:F2}");
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount > 0");
                return;
            }

            if (selectedAccId == -1)
            {
                MessageBox.Show("No account selected.");
                return;
            }

            if (amount > currentBal)
            {
                MessageBox.Show("Insufficient balance.");
                return;
            }

            try
            {
                decimal newBal = currentBal - amount;
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
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
                            SqlCommand logCmd = new SqlCommand(
                                "INSERT INTO transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) VALUES (@aid, 'Withdraw', @amount, GETDATE())",
                                con, transaction);
                            logCmd.Parameters.AddWithValue("@aid", selectedAccId);
                            logCmd.Parameters.AddWithValue("@amount", amount);
                            try { logCmd.ExecuteNonQuery(); } catch { }

                            transaction.Commit();

                            currentBal = newBal;
                            lblBalance.Text = $"Account: {selectedAccId}\nBalance: ${currentBal:F2}";
                            txtAmount.Clear();

                            // 🔹 Refresh grids after withdraw
                            RefreshAllAccountGrids();

                            MessageBox.Show($"Withdraw successful! New Balance: ${newBal:F2}");
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // 🔹 Simple method to refresh any open forms with ReloadAccounts method
        private void RefreshAllAccountGrids()
        {
            foreach (Form form in Application.OpenForms)
            {
                try
                {
                    var method = form.GetType().GetMethod("ReloadAccounts");
                    if (method != null)
                    {
                        method.Invoke(form, null);
                    }
                }
                catch { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerDashboard admins = new CustomerDashboard();
            admins.Show();
            this.Hide();
        }
    }
}
