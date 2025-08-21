using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Customer : Form
    {
        string connectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";
        int selectedCustomerId = -1; // Track selected customer
        private readonly SqlConnection con;

        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomerData(); // safe, runs after form + controls created
        }


        // =========================
        // Save / Add or Update Customer
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

                if (selectedCustomerId == -1)
                {
                    // New Customer → Prevent duplicate username
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE Username=@Username", con);
                    checkCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show("⚠ Username already exists. Choose another one.");
                        return;
                    }

                    // Insert new
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Customers (Customer_Name, Phone, Email, Address, Username, Password) " +
                        "VALUES (@Customer_Name, @Phone, @Email, @Address, @Username, @Password)", con);

                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Customer Saved Successfully");
                }
                else
                {
                    // Update existing
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Customers SET Customer_Name=@Customer_Name, Phone=@Phone, Email=@Email, " +
                        "Address=@Address, Username=@Username, Password=@Password WHERE Customer_ID=@Customer_ID", con);

                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Customer_ID", selectedCustomerId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Customer Updated Successfully");
                }
            }

            LoadCustomerData();
            ClearFields();
        }

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
        // Load Customers to Grid
        // =========================
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

        // =========================
        // Update Button (Load selected row into fields)
        // =========================
        private void btnUpdate_Click(object sender, EventArgs e)
        {// =========================
         // Update existing Customer using Customer_Name
         // =========================
            SqlCommand cmd = new SqlCommand(
                "UPDATE Customers SET Phone=@Phone, Email=@Email, Address=@Address, Username=@Username, Password=@Password " +
                "WHERE Customer_Name=@Customer_Name", con);

            cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                MessageBox.Show("✅ Customer Updated Successfully");
            else
                MessageBox.Show("⚠ No customer found with that name!");
        }


        // =========================
        // Delete Customer
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Customer_ID"].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE Customer_ID=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("🗑 Customer Deleted Successfully");
                LoadCustomerData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("⚠ Please select a customer to delete.");
            }
        }

        // =========================
        // Load on Form Start
        // =========================
        
    }
}
