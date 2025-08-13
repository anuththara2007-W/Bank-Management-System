using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        int customerId;
        string mode; // "Deposit" or "Withdraw"
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public DepositWithdraw(int cid, string modeFlag)
        {
            InitializeComponent();
            customerId = cid;
            mode = modeFlag;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            lblMode.Text = mode;
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string q = "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID = @cid";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAccounts.AutoGenerateColumns = true;
                dgvAccounts.DataSource = dt;
            }
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtAccountID.Text = dgvAccounts.Rows[e.RowIndex].Cells["Account_ID"].Value.ToString();
            txtBalance.Text = dgvAccounts.Rows[e.RowIndex].Cells["Balance"].Value.ToString();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAccountID.Text, out int aid))
            {
                MessageBox.Show("Select an account first.");
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
                    // read current balance (and ensure account belongs to this customer)
                    SqlCommand cmdBal = new SqlCommand("SELECT Balance FROM Accounts WHERE Account_ID = @aid AND Customer_ID = @cid", con, tx);
                    cmdBal.Parameters.AddWithValue("@aid", aid);
                    cmdBal.Parameters.AddWithValue("@cid", customerId);
                    object o = cmdBal.ExecuteScalar();
                    if (o == null)
                    {
                        MessageBox.Show("Account not found or does not belong to you.");
                        tx.Rollback();
                        return;
                    }
                    decimal current = Convert.ToDecimal(o);
                    decimal newBal = current;
                    string txType = mode;

                    if (mode.Equals("Withdraw", StringComparison.OrdinalIgnoreCase))
                    {
                        if (current < amt)
                        {
                            MessageBox.Show("Insufficient funds.");
                            tx.Rollback();
                            return;
                        }
                        newBal = current - amt;
                        txType = "Withdrawal";
                    }
                    else
                    {
                        newBal = current + amt;
                        txType = "Deposit";
                    }

                    // Update balance
                    SqlCommand cmdUpd = new SqlCommand("UPDATE Accounts SET Balance = @bal WHERE Account_ID = @aid", con, tx);
                    cmdUpd.Parameters.AddWithValue("@bal", newBal);
                    cmdUpd.Parameters.AddWithValue("@aid", aid);
                    cmdUpd.ExecuteNonQuery();

                    // Insert transaction (auto TID if identity or supply unique ID)
                    SqlCommand cmdIns = new SqlCommand("INSERT INTO Transactions (Transaction_Type, Amount, Transaction_Date, Account_ID) VALUES (@type, @amt, @date, @aid)", con, tx);
                    cmdIns.Parameters.AddWithValue("@type", txType);
                    cmdIns.Parameters.AddWithValue("@amt", amt);
                    cmdIns.Parameters.AddWithValue("@date", DateTime.Now);
                    cmdIns.Parameters.AddWithValue("@aid", aid);
                    cmdIns.ExecuteNonQuery();

                    tx.Commit();
                    MessageBox.Show("Transaction successful.");
                    LoadAccounts();
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
