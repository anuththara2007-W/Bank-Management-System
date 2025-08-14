using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class DepositWithdraw : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public DepositWithdraw()
        {
            InitializeComponent();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount");
                return;
            }
            ProcessTransaction("Deposit", amount);
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount");
                return;
            }
            ProcessTransaction("Withdraw", -amount);
        }

        private void ProcessTransaction(string type, decimal amount)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand getAccount = new SqlCommand("SELECT TOP 1 Account_ID, Balance FROM Accounts WHERE Customer_ID=@cid", con, tran);
                    getAccount.Parameters.AddWithValue("@cid", Session.CustomerID);
                    var reader = getAccount.ExecuteReader();
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

                    if (amount < 0 && balance + amount < 0)
                    {
                        MessageBox.Show("Insufficient funds.");
                        tran.Rollback();
                        return;
                    }

                    SqlCommand updateBalance = new SqlCommand(
                        "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID=@aid", con, tran);
                    updateBalance.Parameters.AddWithValue("@amt", amount);
                    updateBalance.Parameters.AddWithValue("@aid", accountId);
                    updateBalance.ExecuteNonQuery();

                    SqlCommand insertTran = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Transaction_Date, Account_ID) VALUES (@type, @amt, GETDATE(), @aid)", con, tran);
                    insertTran.Parameters.AddWithValue("@type", type);
                    insertTran.Parameters.AddWithValue("@amt", Math.Abs(amount));
                    insertTran.Parameters.AddWithValue("@aid", accountId);
                    insertTran.ExecuteNonQuery();

                    tran.Commit();
                    MessageBox.Show($"{type} successful.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
