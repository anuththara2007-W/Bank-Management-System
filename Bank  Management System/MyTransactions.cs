using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class MyTransactions : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public MyTransactions()
        {
            InitializeComponent();
        }

        private void MyTransactions_Load(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT TID, Transaction_Type, Amount, Transaction_Date, Account_ID
                                 FROM Transactions 
                                 WHERE Account_ID IN (SELECT Account_ID FROM Accounts WHERE Customer_ID=@cid)";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTransactions.DataSource = dt;
            }
        }
    }
}
