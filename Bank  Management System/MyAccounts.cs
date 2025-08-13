using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class MyAccounts : Form
    {
        int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public MyAccounts(int cid)
        {
            InitializeComponent();
            customerId = cid;
        }

        private void MyAccounts_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string q = "SELECT Account_ID, Account_Type, Balance, Date_Opened FROM Accounts WHERE Customer_ID = @cid";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
            }
        }

        // optionally double-click to show transaction for account
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int accountId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Account_ID"].Value);
            var tx = new MyTransactions(customerId, accountId);
            tx.ShowDialog();
        }
    }
}
