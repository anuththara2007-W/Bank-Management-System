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
                        "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con);
                    cmd.Parameters.AddWithValue("@cid", customerIdToUse);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        selectedAccId = Convert.ToInt32(reader["Account_ID"]);
                        currentBal = Convert.ToDecimal(reader["Balance"]);
                        lblBalance.Text = $"Balance: ${currentBal:F2}";
                        custId = customerIdToUse;
                    }
                    else
                    {
                        MessageBox.Show($"No account found for Customer ID: {customerIdToUse}");
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
            if (custId > 0)
            {
                return custId;
            }

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
                            return sessionCustomerId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Session access error: {ex.Message}");
            }

            return 0;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount.");
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
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con);
                    cmd.Parameters.AddWithValue("@bal", newBal);
                    cmd.Parameters.AddWithValue("@aid", selectedAccId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        currentBal = newBal;
                        lblBalance.Text = $"Balance: ${currentBal:F2}";
                        txtAmount.Clear();

                        // Raise the event
                        BalanceUpdated?.Invoke(selectedAccId, newBal);

                        // Also try to refresh any open forms
                        RefreshAllAccountGrids();

                        MessageBox.Show("Deposit successful!");
                    }
                    else
                    {
                        MessageBox.Show("Deposit failed.");
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
                MessageBox.Show("Enter valid amount.");
                return;
            }

            if (selectedAccId == -1)
            {
                MessageBox.Show("No account selected.");
                return;
            }

            if (amount > currentBal)
            {
                MessageBox.Show("Not enough balance.");
                return;
            }

            try
            {
                decimal newBal = currentBal - amount;
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con);
                    cmd.Parameters.AddWithValue("@bal", newBal);
                    cmd.Parameters.AddWithValue("@aid", selectedAccId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        currentBal = newBal;
                        lblBalance.Text = $"Balance: ${currentBal:F2}";
                        txtAmount.Clear();

                        // Raise the event
                        BalanceUpdated?.Invoke(selectedAccId, newBal);

                        // Also try to refresh any open forms
                        RefreshAllAccountGrids();

                        MessageBox.Show("Withdraw successful!");
                    }
                    else
                    {
                        MessageBox.Show("Withdraw failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void RefreshAllAccountGrids()
        {
            try
            {
                // Method 1: Refresh Owner form if it has a LoadAccounts method
                if (this.Owner != null)
                {
                    // Try to call LoadAccounts or similar method via reflection
                    var loadMethod = this.Owner.GetType().GetMethod("LoadAccounts") ??
                                    this.Owner.GetType().GetMethod("LoadAccountsData") ??
                                    this.Owner.GetType().GetMethod("RefreshAccounts") ??
                                    this.Owner.GetType().GetMethod("RefreshData");

                    if (loadMethod != null)
                    {
                        if (this.Owner.InvokeRequired)
                        {
                            this.Owner.Invoke(new Action(() => loadMethod.Invoke(this.Owner, null)));
                        }
                        else
                        {
                            loadMethod.Invoke(this.Owner, null);
                        }
                    }
                }

                // Method 2: Find and refresh all DataGridViews in all open forms
                foreach (Form form in Application.OpenForms)
                {
                    RefreshFormDataGridViews(form);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing grids: {ex.Message}");
            }
        }

        private void RefreshFormDataGridViews(Form form)
        {
            try
            {
                // Find all DataGridViews in the form
                var dataGridViews = FindAllControls<DataGridView>(form);

                foreach (var dgv in dataGridViews)
                {
                    // Check if this DataGridView likely contains account data
                    if (dgv.Name.ToLower().Contains("account") ||
                        dgv.Name.ToLower().Contains("dgv") ||
                        (dgv.DataSource is System.Data.DataTable dt &&
                         dt.Columns.Contains("Account_ID")))
                    {
                        // Refresh this DataGridView
                        if (form.InvokeRequired)
                        {
                            form.Invoke(new Action(() => LoadAccountsIntoGrid(dgv)));
                        }
                        else
                        {
                            LoadAccountsIntoGrid(dgv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing form grids: {ex.Message}");
            }
        }

        private System.Collections.Generic.List<T> FindAllControls<T>(Control container) where T : Control
        {
            var controls = new System.Collections.Generic.List<T>();

            foreach (Control control in container.Controls)
            {
                if (control is T typedControl)
                {
                    controls.Add(typedControl);
                }

                // Recursively search in child containers
                if (control.HasChildren)
                {
                    controls.AddRange(FindAllControls<T>(control));
                }
            }

            return controls;
        }

        private void LoadAccountsIntoGrid(DataGridView dgv)
        {
            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();

                    string query = @"SELECT 
                                    Account_ID as 'Account ID',
                                    Account_Type as 'Account Type', 
                                    Balance,
                                    Created_Date as 'Created Date'
                                FROM accounts 
                                WHERE Customer_ID = @custId
                                ORDER BY Account_ID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@custId", custId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            System.Data.DataTable dt = new System.Data.DataTable();
                            adapter.Fill(dt);

                            dgv.DataSource = null;
                            dgv.DataSource = dt;

                            // Format the grid
                            if (dgv.Columns["Balance"] != null)
                            {
                                dgv.Columns["Balance"].DefaultCellStyle.Format = "C2";
                                dgv.Columns["Balance"].DefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleRight;
                            }

                            if (dgv.Columns["Created Date"] != null)
                            {
                                dgv.Columns["Created Date"].DefaultCellStyle.Format = "MM/dd/yyyy";
                            }

                            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading grid data: {ex.Message}");
            }
        }
    }
}