using BankApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private int selectedAccountId = -1;

        public DepositWithdraw(int customerID, string v)
        {
            InitializeComponent();
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID = @cid",
                    con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAccounts.DataSource = dt;
            }

            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.MultiSelect = false;

            // Handle selection
            dgvAccounts.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    selectedAccountId = Convert.ToInt32(dgvAccounts.Rows[e.RowIndex].Cells["Account_ID"].Value);
                    decimal balance = Convert.ToDecimal(dgvAccounts.Rows[e.RowIndex].Cells["Balance"].Value);
                    lblBalance.Text = $"Balance: {balance:C}";
                }
            };

            // Auto-select first account
            if (dgvAccounts.Rows.Count > 0)
            {
                dgvAccounts.Rows[0].Selected = true;
                selectedAccountId = Convert.ToInt32(dgvAccounts.Rows[0].Cells["Account_ID"].Value);
                decimal balance = Convert.ToDecimal(dgvAccounts.Rows[0].Cells["Balance"].Value);
                lblBalance.Text = $"Balance: {balance:C}";
            }
            else
            {
                lblBalance.Text = "Balance: 0";
            }
        }

        private void PerformTransaction(string mode)
        {
            if (selectedAccountId == -1)
            {
                MessageBox.Show("Please select an account from the list.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please enter an amount.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Invalid amount.");
                return;
            }

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    string sql = (mode == "deposit")
                        ? "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID = @aid"
                        : "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID = @aid AND Balance >= @amt";

                    SqlCommand cmd = new SqlCommand(sql, con, trans);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new Exception("Insufficient balance or account not found.");

                    // Insert into Transactions
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Transaction_Date, Account_ID, Customer_ID) " +
                        "VALUES (@type, @amt, @date, @aid, @cid)", con, trans);

                    cmd2.Parameters.AddWithValue("@type", mode);
                    cmd2.Parameters.AddWithValue("@amt", amount);
                    cmd2.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd2.Parameters.AddWithValue("@aid", selectedAccountId);
                    cmd2.Parameters.AddWithValue("@cid", Session.CustomerID);
                    cmd2.ExecuteNonQuery();

                    trans.Commit();

                    MessageBox.Show($"{mode} successful!");

                    // Refresh grid + balance
                    LoadAccounts();
                    UpdateBalanceLabel();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void UpdateBalanceLabel()
        {
            if (selectedAccountId == -1) return;

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Balance FROM Accounts WHERE Account_ID = @aid", con);
                cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    decimal balance = Convert.ToDecimal(result);
                    lblBalance.Text = $"Balance: {balance:C}";
                }
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            PerformTransaction("deposit");
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            PerformTransaction("withdraw");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard dash = new CustomerDashboard();
            dash.Show();
            this.Hide();
        }
    }
}
