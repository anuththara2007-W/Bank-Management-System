using BankApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private int accountId = -1;
        private decimal balance = 0m;
        private int customerId;
        private string customerName;

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        public DepositWithdraw(int customerId, string customerName) : this()
        {
            this.customerId = customerId;
            this.customerName = customerName;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void LoadAccount()
        {
            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con);
                    cmd.Parameters.AddWithValue("@cid", customerId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        accountId = Convert.ToInt32(reader["Account_ID"]);
                        balance = Convert.ToDecimal(reader["Balance"]);
                        lblBalance.Text = $"Balance: ${balance:F2}";
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

            if (accountId == -1)
            {
                MessageBox.Show("No account selected.");
                return;
            }

            try
            {
                decimal newBalance = balance + amount;
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con);
                    cmd.Parameters.AddWithValue("@bal", newBalance);
                    cmd.Parameters.AddWithValue("@aid", accountId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        balance = newBalance;
                        lblBalance.Text = $"Balance: ${balance:F2}";
                        txtAmount.Clear();
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

            if (accountId == -1)
            {
                MessageBox.Show("No account selected.");
                return;
            }

            if (amount > balance)
            {
                MessageBox.Show("Not enough balance.");
                return;
            }

            try
            {
                decimal newBalance = balance - amount;
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con);
                    cmd.Parameters.AddWithValue("@bal", newBalance);
                    cmd.Parameters.AddWithValue("@aid", accountId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        balance = newBalance;
                        lblBalance.Text = $"Balance: ${balance:F2}";
                        txtAmount.Clear();
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
    }
}