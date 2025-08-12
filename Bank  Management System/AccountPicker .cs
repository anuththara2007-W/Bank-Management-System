using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class AccountPicker : Form
    {
        public int SelectedAccountID { get; private set; }
        public string ConnectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public AccountPicker()
        {
            InitializeComponent();
        }

        private void AccountPicker_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string query = @"
            SELECT 
                a.Account_ID, 
                a.Account_Type, 
                a.Balance, 
                c.Customer_Name
            FROM Accounts a
            INNER JOIN Customers c ON a.Customer_ID = c.Customer_ID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAccounts.AutoGenerateColumns = true; // ensures all columns show
                dgvAccounts.DataSource = dt;
            }
        }

        private void dgvAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedAccountID = Convert.ToInt32(dgvAccounts.Rows[e.RowIndex].Cells["Account_ID"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow != null)
            {
                SelectedAccountID = Convert.ToInt32(dgvAccounts.CurrentRow.Cells["Account_ID"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
