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
            LoadCustomers();
        }

        // Load all customers into grid
        private void LoadCustomers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT Customer_ID, Customer_Name, phone, Email, Address, Username, Password FROM Customers";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridCustomer.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Save new customer
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" || txtPhoneNo.Text == "" || txtUsername.Text == "")
            {
                MessageBox.Show("Please fill required fields!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Customers (Customer_Name, phone, Email, Address, Username, Password) VALUES (@name, @phone, @email, @address, @username, @password)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer saved!");
                    LoadCustomers();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving: " + ex.Message);
            }
        }

        // Update selected customer
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Select a customer first!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE Customers SET Customer_Name=@name, phone=@phone, Email=@email, Address=@address, Username=@username, Password=@password WHERE Customer_ID=@id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@id", selectedCustomerId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer updated!");
                    LoadCustomers();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating: " + ex.Message);
            }
        }

        // Delete selected customer
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Select a customer first!");
                return;
            }

            if (MessageBox.Show("Delete this customer?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "DELETE FROM Customers WHERE Customer_ID=@id";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", selectedCustomerId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Customer deleted!");
                        LoadCustomers();
                        ClearFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting: " + ex.Message);
                }
            }
        }

        // When user clicks on grid row
        private void GridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = GridCustomer.Rows[e.RowIndex];

                selectedCustomerId = Convert.ToInt32(row.Cells["Customer_ID"].Value);
                txtCustomerName.Text = row.Cells["Customer_Name"].Value.ToString();
                txtPhoneNo.Text = row.Cells["phone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
            }
        }

        // Clear all text fields
        private void ClearFields()
        {
            txtCustomerName.Text = "";
            txtPhoneNo.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            selectedCustomerId = -1;
        }
    }
}