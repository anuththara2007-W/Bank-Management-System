using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class TransferFunds : Form
    {
        int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public TransferFunds(int cid)
        {
            InitializeComponent();
            customerId = cid;
        }

        private void TransferFunds_Load(object sender, EventArgs e)
        {
            LoadMyAccounts();
        }

        private void LoadMyAccounts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string q = "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID = @cid";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvFrom.AutoGenerateColumns = true;
                dgvFrom.DataSource = dt;
            }
        }

        private void dgvFrom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtFromAccount.Text = dgvFrom.Rows[e.RowIndex].Cells["Account_ID"].Value.ToString();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtFromAccount.Text, out int fromAcc))
            {
                MessageBox.Show("Select a source account.");
                return;
            }
            if (!int.TryParse(txtToAccount.Text, out int toAcc))
            {
                MessageBox.Show("Enter a valid destination account ID.");
                return;
            }
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0)
            {
                MessageBox.Show("Enter a valid amount.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    // Ensure source belongs to customer and get balance
                    SqlCommand cmdSrc = new SqlCommand("SELECT Balance FROM Accounts WHERE Account_ID = @aid AND Customer_ID = @cid", con, tx);
                    cmdSrc.Parameters.AddWithValue("@aid", fromAcc);
                    cmdSrc.Parameters.AddWithValue("@cid", customerId);
                    object oSrc = cmdSrc.ExecuteScalar();
                    if (oSrc == null) { MessageBox.Show("Source account not found or not yours."); tx.Rollback(); return; }
                    decimal srcBal = Convert.ToDecimal(oSrc);
                    if (srcBal < amt) { MessageBox.Show("Insufficient funds."); tx.Rollback(); return; }

                    // Ensure dest exists
                    SqlCommand cmdDest = new SqlCommand("SELECT Balance FROM Accounts WHERE Account_ID = @aid", con, tx);
                    cmdDest.Parameters.AddWithValue("@aid", toAcc);
                    object oDest = cmdDest.ExecuteScalar();
                    if (oDest == null) { MessageBox.Show("Destination account not found."); tx.Rollback(); return; }

                    // Update balances
                    SqlCommand updSrc = new SqlCommand("UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID = @aid", con, tx);
                    updSrc.Parameters.AddWithValue("@amt", amt);
                    updSrc.Parameters.AddWithValue("@aid", fromAcc);
                    updSrc.ExecuteNonQuery();

                    SqlCommand updDest = new SqlCommand("UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID = @aid", con, tx);
                    updDest.Parameters.AddWithValue("@amt", amt);
                    updDest.Parameters.AddWithValue("@aid", toAcc);
                    updDest.ExecuteNonQuery();

                    // Insert transactions for both sides
                    SqlCommand ins1 = new SqlCommand("INSERT INTO Transactions (Transaction_Type, Amount, Transaction_Date, Account_ID) VALUES (@t,@a,@d,@acc)", con, tx);
                    ins1.Parameters.AddWithValue("@t", "Transfer Out");
                    ins1.Parameters.AddWithValue("@a", amt);
                    ins1.Parameters.AddWithValue("@d", DateTime.Now);
                    ins1.Parameters.AddWithValue("@acc", fromAcc);
                    ins1.ExecuteNonQuery();

                    SqlCommand ins2 = new SqlCommand("INSERT INTO Transactions (Transaction_Type, Amount, Transaction_Date, Account_ID) VALUES (@t,@a,@d,@acc)", con, tx);
                    ins2.Parameters.AddWithValue("@t", "Transfer In");
                    ins2.Parameters.AddWithValue("@a", amt);
                    ins2.Parameters.AddWithValue("@d", DateTime.Now);
                    ins2.Parameters.AddWithValue("@acc", toAcc);
                    ins2.ExecuteNonQuery();

                    tx.Commit();
                    MessageBox.Show("Transfer successful.");
                    LoadMyAccounts();
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    MessageBox.Show("Transfer failed: " + ex.Message);
                }
            }
        }
    }
}
