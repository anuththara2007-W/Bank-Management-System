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
        private decimal currentBalance = 0;
        private string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        // 🔹 On form load
        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccountsToGrid();
            LoadCustomerAccount();
        }

        // 🔹 Load accounts into DataGridView
        private void LoadAccountsToGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string query = "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID = @cid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gridAccounts.DataSource = dt; // ✅ gridAccounts is your DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

        // 🔹 Load first account balance into label
        private void LoadCustomerAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string query = "SELECT TOP 1 Account_ID, Balance FROM Accounts WHERE Customer_ID = @cid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        selectedAccountId = (int)reader["Account_ID"];
                        currentBalance = (decimal)reader["Balance"];
                        lblBalance.Text = "Balance: " + currentBalance.ToString("C");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading balance: " + ex.Message);
            }
        }

        // 🔹 Deposit money
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID=@aid", con, tran);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                    cmd.ExecuteNonQuery();

                    SqlCommand insertTran = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Account_ID, Customer_ID) " +
                        "VALUES ('Deposit', @amt, @aid, @cid)", con, tran);
                    insertTran.Parameters.AddWithValue("@amt", amount);
                    insertTran.Parameters.AddWithValue("@aid", selectedAccountId);
                    insertTran.Parameters.AddWithValue("@cid", Session.CustomerID);
                    insertTran.ExecuteNonQuery();

                    tran.Commit();

                    MessageBox.Show("Deposit successful.");
                    LoadAccountsToGrid();
                    LoadCustomerAccount();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // 🔹 Withdraw money
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount.");
                return;
            }

            if (amount > currentBalance)
            {
                MessageBox.Show("Insufficient funds.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID=@aid", con, tran);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                    cmd.ExecuteNonQuery();

                    SqlCommand insertTran = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Account_ID, Customer_ID) " +
                        "VALUES ('Withdraw', @amt, @aid, @cid)", con, tran);
                    insertTran.Parameters.AddWithValue("@amt", amount);
                    insertTran.Parameters.AddWithValue("@aid", selectedAccountId);
                    insertTran.Parameters.AddWithValue("@cid", Session.CustomerID);
                    insertTran.ExecuteNonQuery();

                    tran.Commit();

                    MessageBox.Show("Withdraw successful.");
                    LoadAccountsToGrid();
                    LoadCustomerAccount();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // 🔹 Handle DataGridView row selection
        private void gridAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridAccounts.Rows[e.RowIndex];
                selectedAccountId = Convert.ToInt32(row.Cells["Account_ID"].Value);
                currentBalance = Convert.ToDecimal(row.Cells["Balance"].Value);
                lblBalance.Text = "Balance: " + currentBalance.ToString("C");
            }
        }

        // 🔹 Back button
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard dash = new CustomerDashboard();
            dash.Show();
            this.Hide();
        }
    }
}
