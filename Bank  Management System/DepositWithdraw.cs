using BankApp;                 // keep this if DatabaseHelper lives in BankApp
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private int selectedAccountId = -1;
        private decimal currentBalance = 0m;

        public DepositWithdraw()
        {
            InitializeComponent();
            this.Load += DepositWithdraw_Load;          // ensure Load is wired
            btnDeposit.Click += btnDeposit_Click;       // ensure buttons are wired
            btnWithdraw.Click += btnWithdraw_Click;
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
                using (var cmd = new SqlCommand(
                    "SELECT TOP 1 Account_ID, Balance FROM accounts WHERE Customer_ID = @cid", con))
                {
                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            selectedAccountId = Convert.ToInt32(rdr["Account_ID"]);
                            currentBalance = Convert.ToDecimal(rdr["Balance"]);
                            lblBalance.Text = $"Balance: {currentBalance}";
                        }
                        else
                        {
                            MessageBox.Show("No account found for this customer.");
                        }
                    }
                }
            }
        }

        private void UpdateBalance(decimal newBalance)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE accounts SET Balance = @bal WHERE Account_ID = @aid", con))
                {
                    cmd.Parameters.AddWithValue("@bal", newBalance);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);
                    cmd.ExecuteNonQuery();
                }
            }

            currentBalance = newBalance;
            lblBalance.Text = $"Balance: {currentBalance}";
        }

        private bool TryGetAmount(out decimal amount)
        {
            if (!decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid positive amount.");
                return false;
            }
            return true;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!TryGetAmount(out var amount)) return;
            UpdateBalance(currentBalance + amount);
            MessageBox.Show("Deposit successful!");
            txtAmount.Clear();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!TryGetAmount(out var amount)) return;
            if (amount > currentBalance)
            {
                MessageBox.Show("Not enough balance.");
                return;
            }

            // 👇 THIS is the line that was corrupted before
            decimal newBalance = currentBalance - amount;

            UpdateBalance(newBalance);
            MessageBox.Show("Withdraw successful!");
            txtAmount.Clear();
        }
    }
}
