using BankApp;
using System;
using System.Data;
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
            LoadCustomerAccountsIntoGrid();
        }

        // 🔹 Load all accounts into DataGridView
        private void LoadCustomerAccountsIntoGrid()
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
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Account_ID, Account_Type, Balance FROM accounts WHERE Customer_ID=@cid", con);
                    da.SelectCommand.Parameters.AddWithValue("@cid", customerIdToUse);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No accounts found for this customer. Please create an account first.");
                        this.Close();
                        return;
                    }

                    // Bind to DataGridView
                    gridAccounts.DataSource = dt;
                    gridAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    gridAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    gridAccounts.ReadOnly = true;

                    // Select first account by default
                    if (gridAccounts.Rows.Count > 0)
                    {
                        gridAccounts.Rows[0].Selected = true;
                        SelectAccountFromRow(gridAccounts.Rows[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}");
            }
        }

        // 🔹 When user clicks row in grid
        private void gridAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ignore header click
            {
                DataGridViewRow row = gridAccounts.Rows[e.RowIndex];
                SelectAccountFromRow(row);
            }
        }

        private void SelectAccountFromRow(DataGridViewRow row)
        {
            selectedAccId = Convert.ToInt32(row.Cells["Account_ID"].Value);
            currentBal = Convert.ToDecimal(row.Cells["Balance"].Value);
            string accType = row.Cells["Account_Type"].Value.ToString();

            lblBalance.Text = $"Account: {selectedAccId} ({accType})\nBalance: ${currentBal:F2}";
        }

        private int GetCustomerID()
        {
            if (custId > 0)
                return custId;

            try
            {
                var sessionType = Type.GetType("Bank__Management_System.Session") ??
                                 Type.GetType("BankApp.Session");
                if (sessionType != null)
                {
                    var customerIdProperty = sessionType.GetProperty("CustomerID");
                    if (customerIdProperty != null)
                    {
                        object value = customerIdProperty.GetValue(null);
                        if (value != null && int.TryParse(value.ToString(), out int sessionCustomerId))
                        {
                            return sessionCustomerId;
                        }
                    }
                }
            }
            catch { }

            return 0;
        }

        // ✅ Deposit (same as your code but uses selectedAccId/currentBal)
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.");
                return;
            }

            if (selectedAccId == -1)
            {
                MessageBox.Show("Please select an account first.");
                return;
            }

            decimal newBal = currentBal + amount;

            try
            {
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
                                "INSERT INTO transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) " +
                                "VALUES (@aid, 'Deposit', @amount, GETDATE())", con, transaction);
                            logCmd.Parameters.AddWithValue("@aid", selectedAccId);
                            logCmd.Parameters.AddWithValue("@amount", amount);
                            try { logCmd.ExecuteNonQuery(); } catch { }

                            transaction.Commit();

                            currentBal = newBal;
                            lblBalance.Text = $"Account: {selectedAccId}\nBalance: ${currentBal:F2}";
                            txtAmount.Clear();

                            BalanceUpdated?.Invoke(selectedAccId, newBal);
                            LoadCustomerAccountsIntoGrid(); // refresh grid

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
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during deposit: {ex.Message}");
            }
        }

        // ✅ Withdraw (same pattern)
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
