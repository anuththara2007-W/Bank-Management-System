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
            this.Load += AccountPicker_Load; // ensures LoadAccounts runs
        }


        private void AccountPicker_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "SELECT Account_ID, Account_Type, Balance FROM Accounts";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAccounts.DataSource = null; // clear old binding
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
