using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class CustomerPicker : Form
    {
        public int SelectedCustomerID { get; private set; }
        public string ConnectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public CustomerPicker()
        {
            InitializeComponent();
        }

        private void CustomerPicker_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Customer_ID, Customer_Name, Phone, Email FROM Customers";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCustomers.DataSource = dt;
            }
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedCustomerID = Convert.ToInt32(dgvCustomers.Rows[e.RowIndex].Cells["Customer_ID"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                SelectedCustomerID = Convert.ToInt32(dgvCustomers.CurrentRow.Cells["Customer_ID"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dgvCustomers_Click(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
