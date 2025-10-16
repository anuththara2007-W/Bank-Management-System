using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Customer : Form
    {
        string connectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";
        int selectedCustomerId = -1;

        public Customer()
        {
            InitializeComponent();

            // Load data immediately after form initialization
            this.WindowState = FormWindowState.Normal;
            this.Shown += Customer_Shown; // Use Shown event instead of Load
        }

        // Use Shown event - more reliable than Load event
        private void Customer_Shown(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        // Alternative
        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        // Load Customers into Grid 
        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Customer_ID, Customer_Name, phone, Email, Address, Username, Password FROM Customers ORDER BY Customer_ID DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridCustomer.DataSource = dt;

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error: " + ex.Message );

            }
        }

        // =========================
        // Add New Customer
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

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Customers (Customer_Name, phone, Email, Address, Username, Password) " +
                        "VALUES (@Customer_Name, @phone, @Email, @Address, @Username, @Password)", con);

                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text ?? "");
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text ?? "");
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Customer Saved Successfully");
                LoadCustomerData(); // Refresh grid
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}");
            }
        }

        // =========================
        // Update Selected Customer
        // =========================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("⚠ Select a customer from the grid first!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Customers SET Customer_Name=@Customer_Name, phone=@phone, Email=@Email, " +
                        "Address=@Address, Username=@Username, Password=@Password WHERE Customer_ID=@Customer_ID", con);

                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text ?? "");
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text ?? "");
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Customer_ID", selectedCustomerId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Customer Updated Successfully");
                LoadCustomerData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating: {ex.Message}");
            }
        }

        // =========================
        // Delete Selected Customer
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("⚠ Select a customer first!");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this customer?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE Customer_ID=@Customer_ID", con);
                        cmd.Parameters.AddWithValue("@Customer_ID", selectedCustomerId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("🗑 Customer Deleted Successfully");
                    LoadCustomerData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting: {ex.Message}");
                }
            }
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
            selectedCustomerId = -1;
        }

        // =========================
        // Load Selected Customer from Grid
        // =========================
        private void GridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = GridCustomer.Rows[e.RowIndex];

                    // Check if the row has data
                    if (row.Cells["Customer_ID"].Value != null &&
                        !string.IsNullOrEmpty(row.Cells["Customer_ID"].Value.ToString()))
                    {
                        selectedCustomerId = Convert.ToInt32(row.Cells["Customer_ID"].Value);
                        txtCustomerName.Text = row.Cells["Customer_Name"].Value?.ToString() ?? "";
                        txtPhoneNo.Text = row.Cells["phone"].Value?.ToString() ?? "";
                        txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                        txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
                        txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
                        txtPassword.Text = row.Cells["Password"].Value?.ToString() ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting customer: {ex.Message}");
            }
        }

        // =========================
        // Manual Refresh Button
        // =========================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
            MessageBox.Show("Grid refreshed!");
        }

        // =========================
        // Test Database Connection
        // =========================
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    MessageBox.Show("✅ Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Connection failed: {ex.Message}");
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Main admins = new Main();
            admins.Show();
            this.Hide();
        }

        private void Customer_Load_1(object sender, EventArgs e)
        {
            GridCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridStyle.ModernizeGrid(GridCustomer);
        }
    }
}