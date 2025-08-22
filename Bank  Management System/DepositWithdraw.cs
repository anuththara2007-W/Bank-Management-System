using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class DepositWithdraw : Form
    {
        private string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public DepositWithdraw()
        {
            InitializeComponent();

            // 🔹 Ensure form load event is attached
            this.Load += DepositWithdraw_Load;
        }

        // 🔹 Form Load: will auto-load grid
        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccountsToGrid();
        }

        // 🔹 Load all accounts of logged-in customer into grid
        private void LoadAccountsToGrid()
        {
            if (Session.CustomerID == 0)
            {
                MessageBox.Show("Customer not logged in.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID=@cid",
                        con);
                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // 🔹 Make sure your DataGridView is named gridAccounts
                    gridAccounts.DataSource = dt;

                    // Optional: Auto-size columns
                    gridAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

        // 🔹 Optional refresh button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAccountsToGrid();
        }
    }
}
