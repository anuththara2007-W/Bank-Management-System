using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class TransferFunds : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public TransferFunds()
        {
            InitializeComponent();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            // ✅ Validate amount
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // ✅ Get the customer’s account
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

                    // ✅ Check balance
                    if (balance < amount)
                    {
                        MessageBox.Show("Insufficient funds.");
                        tran.Rollback();
                        return;
                    }

                    // ✅ Deduct balance
                    SqlCommand updateBal = new SqlCommand(
                        "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID=@aid", con, tran);
                    updateBal.Parameters.AddWithValue("@amt", amount);
                    updateBal.Parameters.AddWithValue("@aid", accountId);
                    updateBal.ExecuteNonQuery();

                    // ✅ Insert into Transactions (only Transfer Out / Payment)
                    SqlCommand insertTran = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Account_ID, Customer_ID) " +
                        "VALUES ('Payment/Transfer', @amt, @aid, @cid)", con, tran);
                    insertTran.Parameters.AddWithValue("@amt", amount);
                    insertTran.Parameters.AddWithValue("@aid", accountId);
                    insertTran.Parameters.AddWithValue("@cid", Session.CustomerID);
                    insertTran.ExecuteNonQuery();

                    tran.Commit();

                    // ✅ Update balance label on form
                    lblBalance.Text = "Balance: " + (balance - amount).ToString("C");

                    MessageBox.Show("Payment successful.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }

        private void btnTransfer_Click_1(object sender, EventArgs e)
        {

        }
    }
}
