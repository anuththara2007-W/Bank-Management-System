using BankApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        // Database connection string
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        // To track selected account
        int selectedAccountId = -1;
        decimal currentBalance = 0;

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        // Load accounts when form opens
        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    // Get all accounts of this customer
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID=@cid", con);
                    da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Show accounts in grid
                    gridAccounts.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

        // When user clicks on a row in grid
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

        // Deposit button
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (selectedAccountId == -1)
            {
                MessageBox.Show("Please select an account first.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    SqlTransaction tran = con.BeginTransaction();

                    try
                    {
                        // Update balance
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID=@aid", con, tran);
                        cmd.Parameters.AddWithValue("@amt", amount);
                        cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                        cmd.ExecuteNonQuery();

                        // Insert into Transactions
                        SqlCommand cmd2 = new SqlCommand(
                            "INSERT INTO Transactions (Transaction_Type, Amount, Account_ID, Customer_ID) VALUES ('Deposit', @amt, @aid, @cid)", con, tran);
                        cmd2.Parameters.AddWithValue("@amt", amount);
                        cmd2.Parameters.AddWithValue("@aid", selectedAccountId);
                        cmd2.Parameters.AddWithValue("@cid", Session.CustomerID);
                        cmd2.ExecuteNonQuery();

                        tran.Commit();
                        MessageBox.Show("Deposit successful!");

                        // Reload accounts
                        SqlDataAdapter da = new SqlDataAdapter(
                            "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID=@cid", con);
                        da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridAccounts.DataSource = dt;
                    }
                    catch (Exception ex2)
                    {
                        tran.Rollback();
                        MessageBox.Show("Error: " + ex2.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Withdraw button
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (selectedAccountId == -1)
            {
                MessageBox.Show("Please select an account first.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount.");
                return;
            }

            if (amount > currentBalance)
            {
                MessageBox.Show("Not enough balance.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    SqlTransaction tran = con.BeginTransaction();

                    try
                    {
                        // Update balance
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID=@aid", con, tran);
                        cmd.Parameters.AddWithValue("@amt", amount);
                        cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                        cmd.ExecuteNonQuery();

                        // Insert into Transactions
                        SqlCommand cmd2 = new SqlCommand(
                            "INSERT INTO Transactions (Transaction_Type, Amount, Account_ID, Customer_ID) VALUES ('Withdraw', @amt, @aid, @cid)", con, tran);
                        cmd2.Parameters.AddWithValue("@amt", amount);
                        cmd2.Parameters.AddWithValue("@aid", selectedAccountId);
                        cmd2.Parameters.AddWithValue("@cid", Session.CustomerID);
                        cmd2.ExecuteNonQuery();

                        tran.Commit();
                        MessageBox.Show("Withdraw successful!");

                        // Reload accounts
                        SqlDataAdapter da = new SqlDataAdapter(
                            "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID=@cid", con);
                        da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gridAccounts.DataSource = dt;
                    }
                    catch (Exception ex2)
                    {
                        tran.Rollback();
                        MessageBox.Show("Error: " + ex2.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Go back button
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard dash = new CustomerDashboard();
            dash.Show();
            this.Hide();
        }
    }
}
