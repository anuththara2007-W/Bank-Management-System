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
        private int selectedAccountId = -1;

        public DepositWithdraw(int cid, string v)
        {
            InitializeComponent();
            customerId = cid;
        }

        public DepositWithdraw()
        {
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadAccounts();
        }

        // ✅ Setup grid only once
        private void SetupGrid()
        {
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.MultiSelect = false;
            dgvAccounts.ReadOnly = true;
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvAccounts.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    selectedAccountId = Convert.ToInt32(dgvAccounts.Rows[e.RowIndex].Cells["Account_ID"].Value);
                    decimal balance = Convert.ToDecimal(dgvAccounts.Rows[e.RowIndex].Cells["Balance"].Value);
                    lblBalance.Text = $"Balance: {balance:C}";
                }
            };
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
                dgvAccounts.DataSource = dt;
            }

            // ✅ Reselect account if already selected
            if (selectedAccountId != -1)
            {
                foreach (DataGridViewRow row in dgvAccounts.Rows)
                {
                    if (Convert.ToInt32(row.Cells["Account_ID"].Value) == selectedAccountId)
                    {
                        row.Selected = true;
                        lblBalance.Text = $"Balance: {row.Cells["Balance"].Value:C}";
                        break;
                    }
                }
            }
            else if (dgvAccounts.Rows.Count > 0)
            {
                dgvAccounts.Rows[0].Selected = true;
                selectedAccountId = Convert.ToInt32(dgvAccounts.Rows[0].Cells["Account_ID"].Value);
                lblBalance.Text = $"Balance: {dgvAccounts.Rows[0].Cells["Balance"].Value:C}";
            }
        }

        private void PerformTransaction(string mode)
        {
            if (selectedAccountId == -1)
            {
                MessageBox.Show("Please select an account from the list.");
                return;
            }
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Invalid amount.");
                return;
            }

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    string sql = (mode == "deposit")
                        ? "UPDATE Accounts SET Balance = Balance + @amt WHERE Account_ID = @aid"
                        : "UPDATE Accounts SET Balance = Balance - @amt WHERE Account_ID = @aid AND Balance >= @amt";

                    SqlCommand cmd = new SqlCommand(sql, con, trans);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@aid", selectedAccountId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0) throw new Exception("Insufficient balance or account not found.");

                    // Insert transaction record
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

                    // ✅ Refresh accounts & balance
                    LoadAccounts();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            PerformTransaction("deposit");
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            PerformTransaction("withdraw");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard dash = new CustomerDashboard();
            dash.Show();
            this.Hide();
        }
    }
}
