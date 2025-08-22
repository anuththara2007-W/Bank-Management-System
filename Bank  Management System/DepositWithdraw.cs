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
        }

        // 🔹 Load event of the form
        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            LoadAccountsToGrid();
        }

        // 🔹 Method to load accounts into grid
        private void LoadAccountsToGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    // load all accounts of logged in customer
                    string query = "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID = @cid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gridAccounts.DataSource = dt; // 🔹 bind to grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

        // 🔹 Call this method whenever deposit/withdraw is done
        private void RefreshGrid_Click(object sender, EventArgs e)
        {
            LoadAccountsToGrid();
        }
    }
}
