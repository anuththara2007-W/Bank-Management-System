using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Customer : Form
    {
        string connectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Customer()
        {
            InitializeComponent();
        }

        // Save / Add
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if ID exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE Customer_ID = @cid", con);
                checkCmd.Parameters.AddWithValue("@cid", txtCustomerID.Text);
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("Customer ID already exists!");
                    return;
                }

                // Insert new customer
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Customer (Customer_ID, Customer_Name, Phone, Email, Address, Username, Password) " +
                    "VALUES (@Customer_ID, @Customer_Name, @Phone, @Email, @Address, @Username, @Password)", con);

                cmd.Parameters.AddWithValue("@Customer_ID", int.Parse(txtCustomerID.Text));
                cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Customer Saved Successfully");
            LoadCustomerData();
        }

        // Load / Refresh
        private void LoadCustomerData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);

                dataGridView1.DataSource = table;
            }
        }

        // Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customer SET Customer_Name = @Customer_Name, Phone = @Phone, Email = @Email, Address = @Address, Username = @Username, Password = @Password " +
                    "WHERE Customer_ID = @Customer_ID", con);

                cmd.Parameters.AddWithValue("@Customer_ID", int.Parse(txtCustomerID.Text));
                cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Record Updated Successfully");
            LoadCustomerData();
        }

        // Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE Customer_ID = @Customer_ID", con);
                cmd.Parameters.AddWithValue("@Customer_ID", int.Parse(txtCustomerID.Text));

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Record Deleted Successfully");
            LoadCustomerData();
        }

        // Form Load
        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();

            // Optional: populate fields when clicking on grid row
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        // Load clicked row into textboxes
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtCustomerID.Text = row.Cells["Customer_ID"].Value.ToString();
                txtCustomerName.Text = row.Cells["Customer_Name"].Value.ToString();
                txtPhoneNo.Text = row.Cells["Phone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
            }
        }

        // Back to main menu
        private void btnBack_Click(object sender, EventArgs e)
        {
            Main admin = new Main();
            admin.Show();
            this.Hide();
        }
    }
}
