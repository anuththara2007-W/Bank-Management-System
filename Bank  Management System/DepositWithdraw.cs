using BankApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private int selectedAccountId = -1;
        private decimal currentBalance = 0;

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void LoadAccount()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        selectedAccountId = Convert.ToInt32(reader["Account_ID"]);
                        currentBalance = Convert.ToDecimal(reader["Balance"]);
                        lblBalance.Text = $"Balance: {currentBalance}";
                    }
                    else
                    {
                        MessageBox.Show("No account found for this customer.");
                    }
                }
            }
        }

        private void UpdateBalance(decimal newBalance)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE accounts SET Balance = @bal WHERE Account_ID = @aid", con);
                cmd.Parameters.AddWithValue("@bal", newBalance);
                cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                cmd.ExecuteNonQuery();
            }

            currentBalance = newBalance;
            lblBalance.Text = $"Balance: {currentBalance}";
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid positive amount.");
                return;
            }

            decimal newBalance = currentBalance + amount;
            UpdateBalance(newBalance);

            MessageBox.Show("✅ Deposit successful!");
            txtAmount.Clear();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid positive amount.");
                return;
            }

            if (amount > currentBalance)
            {
                MessageBox.Show("⚠ Not enough balance.");
                return;
            }

            decimal newBalance = currentBalance - amount;
            UpdateBalance(newBalance);

            MessageBox.Show("✅ Withdraw successful!");
            txtAmount.Clear();
        }
    }
}
