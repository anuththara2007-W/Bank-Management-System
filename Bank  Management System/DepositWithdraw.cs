using BankApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        // 👇 these two variables MUST exist here
        private int selectedAccountId = -1;
        private decimal currentBalance = 0m;

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            // load first account for logged in customer
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    selectedAccountId = Convert.ToInt32(reader["Account_ID"]);
                    currentBalance = Convert.ToDecimal(reader["Balance"]);
                    lblBalance.Text = $"Balance: {currentBalance}";
                }
                else
                {
                    MessageBox.Show("No account found.");
                }
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount.");
                return;
            }

            decimal newBalance = currentBalance + amount;

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con);
                cmd.Parameters.AddWithValue("@bal", newBalance);
                cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                cmd.ExecuteNonQuery();
            }

            currentBalance = newBalance;
            lblBalance.Text = $"Balance: {currentBalance}";
            txtAmount.Clear();
            MessageBox.Show("Deposit successful!");
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
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

            decimal newBalance = currentBalance - amount;

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE accounts SET Balance=@bal WHERE Account_ID=@aid", con);
                cmd.Parameters.AddWithValue("@bal", newBalance);
                cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                cmd.ExecuteNonQuery();
            }

            currentBalance = newBalance;
            lblBalance.Text = $"Balance: {currentBalance}";
            txtAmount.Clear();
            MessageBox.Show("Withdraw successful!");
        }
    }
}
