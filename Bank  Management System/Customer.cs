using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Customer : Form
    {
        string connectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";
        int selectedCustomerId = -1; // Track selected customer

        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomerData(); // Load grid on form load
        }

        // =========================
        // Load / Refresh Customers
        // =========================
        private void LoadCustomerData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);

                dataGridView1.DataSource = table;

                // Optional: hide Customer_ID if you want
                // dataGridView1.Columns["Customer_ID"].Visible = false;
            }
        }

        // =========================
        // Save / Add New Customer
        // =========================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNo.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("⚠ Please fill all required fields!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Customer (Customer_Name, Phone, Email, Address, Username, Password) " +
                    "VALUES (@Customer_Name, @Phone, @Email, @Address, @Username, @Password)", con);

                cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("✅ Customer Saved Successfully");
            LoadCustomerData();
            ClearFields();
        }

        // =========================
        // Update Customer
        // =========================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("⚠ Please select a customer from the grid to update!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customer SET Customer_Name=@Customer_Name, Phone=@Phone, Email=@Email, " +
                    "Address=@Address, Username=@Username, Password=@Password WHERE Customer_ID=@Customer_ID", con);

                cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Customer_ID", selectedCustomerId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("✅ Customer Updated Successfully");
            LoadCustomerData();
            ClearFields();
        }

        // =========================
        // Delete Customer
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("⚠ Please select a customer from the grid to delete!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE Customer_ID=@Customer_ID", con);
                cmd.Parameters.AddWithValue("@Customer_ID", selectedCustomerId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("🗑 Customer Deleted Successfully");
            LoadCustomerData();
            ClearFields();
        }

        // =========================
        // Clear input fields
        // =========================
        private void ClearFields()
        {
            txtCustomerName.Clear();
            txtPhoneNo.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            selectedCustomerId = -1; // Reset selection
        }

        // =========================
        // DataGridView Row Click
        // Load selected customer into input fields
        // =========================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ignore header
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedCustomerId = Convert.ToInt32(row.Cells["Customer_ID"].Value);
                txtCustomerName.Text = row.Cells["Customer_Name"].Value.ToString();
                txtPhoneNo.Text = row.Cells["Phone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
            }
        }
    }
}
