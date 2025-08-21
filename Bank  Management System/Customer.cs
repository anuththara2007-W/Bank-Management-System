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
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            SetupGrid(); // Setup grid structure first
            LoadCustomerData(); // Load grid on form load
        }

        // =========================
        // Setup Grid Structure
        // =========================
        private void SetupGrid()
        {
            try
            {
                // Clear existing columns
                GridCustomer.Columns.Clear();

                // Add columns manually to ensure grid shows even when empty
                GridCustomer.Columns.Add("Customer_ID", "ID");
                GridCustomer.Columns.Add("Customer_Name", "Name");
                GridCustomer.Columns.Add("phone", "Phone");
                GridCustomer.Columns.Add("Email", "Email");
                GridCustomer.Columns.Add("Address", "Address");
                GridCustomer.Columns.Add("Username", "Username");
                GridCustomer.Columns.Add("Password", "Password");

                // Set column widths
                GridCustomer.Columns["Customer_ID"].Width = 60;
                GridCustomer.Columns["Customer_Name"].Width = 150;
                GridCustomer.Columns["phone"].Width = 120;
                GridCustomer.Columns["Email"].Width = 180;
                GridCustomer.Columns["Address"].Width = 200;
                GridCustomer.Columns["Username"].Width = 100;
                GridCustomer.Columns["Password"].Width = 100;

                // Make ID column read-only
                GridCustomer.Columns["Customer_ID"].ReadOnly = true;

                // Set grid properties
                GridCustomer.AllowUserToAddRows = false;
                GridCustomer.AllowUserToDeleteRows = false;
                GridCustomer.ReadOnly = true;
                GridCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                GridCustomer.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up grid: {ex.Message}");
            }
        }

        // =========================
        // Load Customers into Grid
        // =========================
        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Customer_ID, Customer_Name, phone, Email, Address, Username, Password FROM Customers ORDER BY Customer_ID DESC", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Clear existing rows
                    GridCustomer.Rows.Clear();

                    // Add data to grid manually
                    foreach (DataRow row in dt.Rows)
                    {
                        GridCustomer.Rows.Add(
                            row["Customer_ID"],
                            row["Customer_Name"],
                            row["phone"],
                            row["Email"],
                            row["Address"],
                            row["Username"],
                            row["Password"]
                        );
                    }

                    // Update status
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show($"✅ Loaded {dt.Rows.Count} customers");
                    }
                    else
                    {
                        MessageBox.Show("📋 No customers found. Grid is ready for new entries.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error loading customer data: {ex.Message}");
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

                    // Fixed table name and column name (phone instead of Phone)
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Customers (Customer_Name, phone, Email, Address, Username, Password) " +
                        "VALUES (@Customer_Name, @phone, @Email, @Address, @Username, @Password)", con);

                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhoneNo.Text);  // Fixed parameter name
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}");
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

                    // Fixed table name and column name
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Customers SET Customer_Name=@Customer_Name, phone=@phone, Email=@Email, " +
                        "Address=@Address, Username=@Username, Password=@Password WHERE Customer_ID=@Customer_ID", con);

                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhoneNo.Text);  // Fixed parameter name
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer: {ex.Message}");
            }
        }

        // =========================
        // Delete Selected Customer
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
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

                    // Fixed table name
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Customers WHERE Customer_ID=@Customer_ID", con);
                    cmd.Parameters.AddWithValue("@Customer_ID", selectedCustomerId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("🗑 Customer Deleted Successfully");
                LoadCustomerData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}");
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
                if (e.RowIndex >= 0 && GridCustomer.Rows[e.RowIndex].Cells["Customer_ID"].Value != null)
                {
                    DataGridViewRow row = GridCustomer.Rows[e.RowIndex];
                    selectedCustomerId = Convert.ToInt32(row.Cells["Customer_ID"].Value);
                    txtCustomerName.Text = row.Cells["Customer_Name"].Value?.ToString() ?? "";
                    txtPhoneNo.Text = row.Cells["phone"].Value?.ToString() ?? "";
                    txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                    txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
                    txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
                    txtPassword.Text = row.Cells["Password"].Value?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}");
            }
        }

        // =========================
        // Add Refresh Button Method (Optional)
        // =========================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
        }
    }
}