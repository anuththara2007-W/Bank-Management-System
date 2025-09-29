using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BankApp
{
    public partial class TransferFunds : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public TransferFunds()
        {
            InitializeComponent();
            this.Load += TransferFunds_Load;  // ✅ ensure form load is called
        }

        // ✅ Form Load: show balance + load transactions
        private void TransferFunds_Load(object sender, EventArgs e)
        {
            LoadBalance();
            LoadTransactions();
        }

        // ✅ Load customer balance
        private void LoadBalance()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 Balance FROM Accounts WHERE Customer_ID=@cid", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    decimal balance = (decimal)result;
                    lblBalance.Text = "Balance: " + balance.ToString("C");
                }
                else
                {
                    lblBalance.Text = "Balance: N/A";
                }
            }
        }

        // ✅ Load transactions into grid
        private void LoadTransactions()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT TID, Transaction_Type, Amount, Transaction_Date, Purpose, ToAccountNo " +
                    "FROM Transactions WHERE Customer_ID=@cid ORDER BY Transaction_Date DESC", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TransactionsGrid.DataSource = dt;
            }
        }

        // ✅ Transfer / Payment button
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            // Validate amount
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount");
                return;
            }

            string purpose = txtPurpose.Text.Trim();
            string toAccountNo = txtToAccount.Text.Trim();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // Get customer account
                    SqlCommand getAcc = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance FROM Accounts WHERE Customer_ID=@cid", con, tran);
                    getAcc.Parameters.AddWithValue("@cid", Session.CustomerID);

                    SqlDataReader reader = getAcc.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("No account found.");
                        reader.Close();
                        tran.Rollback();
                        return;
                    }

                    int accountId = (int)reader["Account_ID"];
                    decimal balance = (decimal)reader["Balance"];
                    reader.Close();

                    // Check balance
                    if (balance < amount)
                    {
                        MessageBox.Show("Insufficient funds.");
                        tran.Rollback();
                        return;
                    }

                    // Deduct balance
                    SqlCommand updateBal = new SqlCommand(
                        "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID=@aid", con, tran);
                    updateBal.Parameters.AddWithValue("@amt", amount);
                    updateBal.Parameters.AddWithValue("@aid", accountId);
                    updateBal.ExecuteNonQuery();

                    // Insert into Transactions with Purpose + ToAccountNo
                    SqlCommand insertTran = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Account_ID, Customer_ID, Purpose, ToAccountNo) " +
                        "VALUES ('Payment/Transfer', @amt, @aid, @cid, @purpose, @toAcc)", con, tran);

                    insertTran.Parameters.AddWithValue("@amt", amount);
                    insertTran.Parameters.AddWithValue("@aid", accountId);
                    insertTran.Parameters.AddWithValue("@cid", Session.CustomerID);
                    insertTran.Parameters.AddWithValue("@purpose", string.IsNullOrEmpty(purpose) ? (object)DBNull.Value : purpose);
                    insertTran.Parameters.AddWithValue("@toAcc", string.IsNullOrEmpty(toAccountNo) ? (object)DBNull.Value : toAccountNo);

                    insertTran.ExecuteNonQuery();

                    tran.Commit();

                    // Update balance + grid
                    lblBalance.Text = "Balance: RS " + (balance - amount).ToString("C");
                    LoadTransactions();

                    MessageBox.Show("Payment successful.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // ✅ Go back button
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }

        private void TransferFunds_Load_1(object sender, EventArgs e)
        {
            TransactionsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridStyle.ModernizeGrid(TransactionsGrid);

            lblBalance.BorderStyle = BorderStyle.None;
            lblBalance.BackColor = Color.Transparent;
            lblPurpose.BorderStyle = BorderStyle.None;
            lblPurpose.BackColor = Color.Transparent;
            lblToAccount.BorderStyle = BorderStyle.None;
            lblToAccount.BackColor = Color.Transparent;
            lblAmount.BorderStyle = BorderStyle.None;
            lblAmount.BackColor = Color.Transparent;
        }
    }
}
