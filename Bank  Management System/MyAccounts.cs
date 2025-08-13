using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class MyAccounts : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public MyAccounts()
        {
            InitializeComponent();
        }

        private void MyAccounts_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID=@cid";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAccounts.DataSource = dt;
            }
        }
    }
}
