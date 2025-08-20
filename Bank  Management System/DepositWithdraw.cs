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
        }

        private void LoadCustomerAccount()
        {
            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con);
                    cmd.Parameters.AddWithValue("@cid", custId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        selectedAccId = Convert.ToInt32(reader["Account_ID"]);
                        currentBal = Convert.ToDecimal(reader["Balance"]);
                        lblBalance.Text = $"Balance: ${currentBal:F2}";
                    }
                    else
                    {
                        MessageBox.Show("No account found.");
                        this.Close();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                this.Close();
            }
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
                        RefreshAccountsDataGridView();
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
                        RefreshAccountsDataGridView();
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

        private void RefreshAccountsDataGridView()
        {
            try
            {
                // Find the dgvAccounts DataGridView in parent form or current form
                DataGridView dgvAccounts = FindDataGridView();

                if (dgvAccounts != null)
                {
                    // Refresh the DataGridView with updated account data
                    LoadAccountsData(dgvAccounts);
                }
            }
            catch (Exception ex)
            {
                // Silent error handling - don't show message to user for refresh errors
                System.Diagnostics.Debug.WriteLine($"Error refreshing DataGridView: {ex.Message}");
            }
        }

        private DataGridView FindDataGridView()
        {
            // Method 1: Try to find dgvAccounts in current form
            foreach (Control control in this.Controls)
            {
                if (control is DataGridView dgv && dgv.Name == "dgvAccounts")
                {
                    return dgv;
                }
            }

            // Method 2: Try to find dgvAccounts in parent/owner form
            if (this.Owner != null)
            {
                foreach (Control control in this.Owner.Controls)
                {
                    if (control is DataGridView dgv && dgv.Name == "dgvAccounts")
                    {
                        return dgv;
                    }
                }
            }

            // Method 3: Try to find in any open form with dgvAccounts
            foreach (Form openForm in Application.OpenForms)
            {
                foreach (Control control in openForm.Controls)
                {
                    if (control is DataGridView dgv && dgv.Name == "dgvAccounts")
                    {
                        return dgv;
                    }
                }
            }

            return null;
        }

        private void LoadAccountsData(DataGridView dgvAccounts)
        {
            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();

                    // Load accounts for the current customer
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

                            // Update DataGridView on UI thread
                            if (dgvAccounts.InvokeRequired)
                            {
                                dgvAccounts.Invoke(new Action(() => {
                                    dgvAccounts.DataSource = dt;
                                    FormatDataGridView(dgvAccounts);
                                }));
                            }
                            else
                            {
                                dgvAccounts.DataSource = dt;
                                FormatDataGridView(dgvAccounts);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading accounts data: {ex.Message}");
            }
        }

        private void FormatDataGridView(DataGridView dgv)
        {
            try
            {
                // Format the Balance column as currency
                if (dgv.Columns["Balance"] != null)
                {
                    dgv.Columns["Balance"].DefaultCellStyle.Format = "C2";
                    dgv.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Format the Created Date column
                if (dgv.Columns["Created Date"] != null)
                {
                    dgv.Columns["Created Date"].DefaultCellStyle.Format = "MM/dd/yyyy";
                }

                // Auto-resize columns
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error formatting DataGridView: {ex.Message}");
            }
        }
    }
}