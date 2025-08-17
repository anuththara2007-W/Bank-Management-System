using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class DepositWithdraw : Form
    {
        private int customerId;
        private string mode; // "deposit" or "withdraw"
        private int selectedAccountId = -1;

        public DepositWithdraw(int cid, string operation)
        {
            InitializeComponent();
            customerId = cid;
            mode = operation;
        }

        public DepositWithdraw()
        {
            InitializeComponent();
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
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID = @cid", con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAccounts.DataSource = dt; // ✅ Correct usage
            }

            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.MultiSelect = false;

            dgvAccounts.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    selectedAccountId = Convert.ToInt32(dgvAccounts.Rows[e.RowIndex].Cells["Account_ID"].Value);
                }
            };

            // ✅ Auto-select first account if only one exists
            if (dgvAccounts.Rows.Count == 1)
            {
                dgvAccounts.Rows[0].Selected = true;
                selectedAccountId = Convert.ToInt32(dgvAccounts.Rows[0].Cells["Account_ID"].Value);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (selectedAccountId == -1)
            {
                MessageBox.Show("Please select an account from the list.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please enter an amount.");
                return;
            }

            decimal amount = Convert.ToDecimal(txtAmount.Text);

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    // Update balance
                    string sql = (mode == "deposit")
                        ? "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID = @aid"
                        : "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID = @aid AND Balance >= @amt";

                    SqlCommand cmd = new SqlCommand(sql, con, trans);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new Exception("Insufficient balance or account not found.");

                    // Insert into Transactions
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO Transactions (Account_ID, Customer_ID, Transaction_Type, Amount, Transaction_Date) " +
                        "VALUES (@aid, @cid, @type, @amt, @date)", con, trans);

                    cmd2.Parameters.AddWithValue("@aid", selectedAccountId);
                    cmd2.Parameters.AddWithValue("@cid", customerId);
                    cmd2.Parameters.AddWithValue("@type", mode);
                    cmd2.Parameters.AddWithValue("@amt", amount);
                    cmd2.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd2.ExecuteNonQuery();

                    trans.Commit();
                    MessageBox.Show($"{mode} successful!");
                    LoadAccounts(); // refresh balance in grid
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

        private void btnDeposit_Click(object sender, EventArgs e)
        {

        }
    }
}
