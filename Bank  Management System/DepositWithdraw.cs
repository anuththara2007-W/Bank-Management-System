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

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID=@cid", con);
                    da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gridAccounts.DataSource = dt; // ✅ use gridAccounts
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

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

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount");
                return;
            }

            ExecuteTransaction("Deposit", amount);
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount");
                return;
            }

            if (amount > currentBalance)
            {
                MessageBox.Show("Insufficient funds");
                return;
            }

            ExecuteTransaction("Withdraw", amount);
        }

        private void ExecuteTransaction(string type, decimal amount)
        {
            if (selectedAccountId == -1)
            {
                MessageBox.Show("Select an account first.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    string updateQuery = type == "Deposit"
                        ? "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID=@aid"
                        : "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID=@aid";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, con, tran);
                    updateCmd.Parameters.AddWithValue("@amt", amount);
                    updateCmd.Parameters.AddWithValue("@aid", selectedAccountId);
                    updateCmd.ExecuteNonQuery();

                    SqlCommand insertTran = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Account_ID, Customer_ID) " +
                        "VALUES (@type, @amt, @aid, @cid)", con, tran);
                    insertTran.Parameters.AddWithValue("@type", type);
                    insertTran.Parameters.AddWithValue("@amt", amount);
                    insertTran.Parameters.AddWithValue("@aid", selectedAccountId);
                    insertTran.Parameters.AddWithValue("@cid", Session.CustomerID);
                    insertTran.ExecuteNonQuery();

                    tran.Commit();

                    MessageBox.Show(type + " successful.");
                    LoadAccounts(); // ✅ refresh grid
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard dash = new CustomerDashboard();
            dash.Show();
            this.Hide();
        }
    }
}
