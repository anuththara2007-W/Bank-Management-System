using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class DepositWithdraw : Form
    {
        private int customerId;
        private string mode; // "deposit" or "withdraw"

        public DepositWithdraw(int cid, string operation)
        {
            InitializeComponent();
            customerId = cid;
            mode = operation;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            lblMode.Text = (mode == "deposit") ? "Deposit Money" : "Withdraw Money";
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT Account_ID FROM Accounts WHERE Customer_ID = @cid", con);
                cmd.Parameters.AddWithValue("@cid", customerId);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbAccounts.Items.Add(dr["Account_ID"].ToString());
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbAccounts.SelectedItem == null || string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please select an account and enter an amount.");
                return;
            }

            int accountId = Convert.ToInt32(cmbAccounts.SelectedItem);
            decimal amount = Convert.ToDecimal(txtAmount.Text);

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    // 1. Update balance
                    string sql = (mode == "deposit")
                        ? "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID = @aid"
                        : "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID = @aid AND Balance >= @amt";

                    SqlCommand cmd = new SqlCommand(sql, con, trans);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@aid", accountId);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        throw new Exception("Insufficient balance or account not found.");
                    }

                    // 2. Insert transaction record
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO Transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) " +
                        "VALUES (@aid, @type, @amt, @date)", con, trans);

                    cmd2.Parameters.AddWithValue("@aid", accountId);
                    cmd2.Parameters.AddWithValue("@type", mode);
                    cmd2.Parameters.AddWithValue("@amt", amount);
                    cmd2.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd2.ExecuteNonQuery();

                    trans.Commit();
                    MessageBox.Show($"{mode} successful!");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard dash = new CustomerDashboard();
            dash.Show();
            this.Hide();
        }

        private void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
