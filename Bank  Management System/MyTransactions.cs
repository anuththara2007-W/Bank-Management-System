using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class MyTransactions : Form
    {
        int customerId;
        int accountIdFilter;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public MyTransactions(int cid) : this(cid, -1) { }
        public MyTransactions(int cid, int accountId)
        {
            InitializeComponent();
            customerId = cid;
            accountIdFilter = accountId;
        }

        private void MyTransactions_Load(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string q = @"SELECT T.TID, T.Transaction_Type, T.Amount, T.Transaction_Date, T.Account_ID
                             FROM Transactions T
                             JOIN Accounts A ON T.Account_ID = A.Account_ID
                             WHERE A.Customer_ID = @cid";
                if (accountIdFilter > 0) q += " AND T.Account_ID = @aid";
                q += " ORDER BY T.Transaction_Date DESC";

                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);
                if (accountIdFilter > 0) da.SelectCommand.Parameters.AddWithValue("@aid", accountIdFilter);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
            }
        }
    }
}
