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
            if (!decimal.TryParse(txtToAccountID.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount");
                return;
            }
            if (!int.TryParse(txtToAccountID.Text, out int toAccount))
            {
                MessageBox.Show("Enter valid destination account");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand getFrom = new SqlCommand(
                        "SELECT TOP 1 Account_ID, Balance FROM Accounts WHERE Customer_ID=@cid", con, tran);
                    getFrom.Parameters.AddWithValue("@cid", Session.CustomerID);
                    var reader = getFrom.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("No account found.");
                        reader.Close();
                        tran.Rollback();
                        return;
                    }
                    int fromAccount = (int)reader["Account_ID"];
                    decimal balance = (decimal)reader["Balance"];
                    reader.Close();

                    if (balance < amount)
                    {
                        MessageBox.Show("Insufficient funds.");
                        tran.Rollback();
                        return;
                    }

                    SqlCommand debit = new SqlCommand(
                        "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID=@aid", con, tran);
                    debit.Parameters.AddWithValue("@amt", amount);
                    debit.Parameters.AddWithValue("@aid", fromAccount);
                    debit.ExecuteNonQuery();

                    SqlCommand credit = new SqlCommand(
                        "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID=@aid", con, tran);
                    credit.Parameters.AddWithValue("@amt", amount);
                    credit.Parameters.AddWithValue("@aid", toAccount);
                    if (credit.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("Destination account not found.");
                        tran.Rollback();
                        return;
                    }

                    SqlCommand insertFrom = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Transaction_Date, Account_ID) VALUES ('Transfer Out', @amt, GETDATE(), @aid)", con, tran);
                    insertFrom.Parameters.AddWithValue("@amt", amount);
                    insertFrom.Parameters.AddWithValue("@aid", fromAccount);
                    insertFrom.ExecuteNonQuery();

                    SqlCommand insertTo = new SqlCommand(
                        "INSERT INTO Transactions (Transaction_Type, Amount, Transaction_Date, Account_ID) VALUES ('Transfer In', @amt, GETDATE(), @aid)", con, tran);
                    insertTo.Parameters.AddWithValue("@amt", amount);
                    insertTo.Parameters.AddWithValue("@aid", toAccount);
                    insertTo.ExecuteNonQuery();

                    tran.Commit();
                    MessageBox.Show("Transfer successful.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
