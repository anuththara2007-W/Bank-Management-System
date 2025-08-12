using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Customer : Form
    {
        // single source of truth for the connection and table name
        string connectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";
        string customerTableName = null; // will be set on load

        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            try
            {
                customerTableName = DetermineCustomerTableName();
                if (string.IsNullOrEmpty(customerTableName))
                {
                    MessageBox.Show("No Customer table found in the database. Expected table name 'Customer' or 'Customers'.", "Table not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LoadCustomerData();

                // wire DataGridView click (if not wired in designer)
                dataGridView1.CellClick += DataGridView1_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while initializing Customer form: " + ex.Message);
            }
        }

        // Determine whether the DB uses "Customer" or "Customers" (or pick the one with rows)
        private string DetermineCustomerTableName()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // find if either table exists
                SqlCommand cmd = new SqlCommand(
                    "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Customer','Customers') AND TABLE_TYPE='BASE TABLE'", con);

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0)
                    return null;

                if (dt.Rows.Count == 1)
                    return dt.Rows[0]["TABLE_NAME"].ToString();

                // if both exist pick the one with more rows (prefer non-empty)
                int countCustomer = GetRowCountForTable("Customer");
                int countCustomers = GetRowCountForTable("Customers");

                return (countCustomers >= countCustomer) ? "Customers" : "Customer";
            }
        }

        private int GetRowCountForTable(string tbl)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand c = new SqlCommand($"SELECT COUNT(*) FROM [{tbl}]", con);
                    object o = c.ExecuteScalar();
                    return Convert.ToInt32(o);
                }
            }
            catch
            {
                return 0;
            }
        }

        // Load / Refresh
        private void LoadCustomerData()
        {
            if (string.IsNullOrEmpty(customerTableName)) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM [{customerTableName}]", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading customer data: " + ex.Message);
                }
            }
        }

        // Save / Add
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customerTableName))
            {
                MessageBox.Show("Customer table not found in DB.");
                return;
            }

            if (!int.TryParse(txtCustomerID.Text.Trim(), out int newId))
            {
                MessageBox.Show("Customer ID must be a valid integer.");
                txtCustomerID.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check if ID exists
                    SqlCommand checkCmd = new SqlCommand($"SELECT COUNT(*) FROM [{customerTableName}] WHERE Customer_ID = @cid", con);
                    checkCmd.Parameters.AddWithValue("@cid", newId);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show("Customer ID already exists!");
                        return;
                    }

                    // Insert new customer
                    SqlCommand cmd = new SqlCommand(
                        $"INSERT INTO [{customerTableName}] (Customer_ID, Customer_Name, Phone, Email, Address, Username, Password) " +
                        "VALUES (@Customer_ID, @Customer_Name, @Phone, @Email, @Address, @Username, @Password)", con);

                    cmd.Parameters.AddWithValue("@Customer_ID", newId);
                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Customer Saved Successfully");
                        ClearInputs();
                        LoadCustomerData();
                    }
                    else
                    {
                        MessageBox.Show("Customer save returned 0 rows affected.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving customer: " + ex.Message);
            }
        }

        // Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customerTableName))
            {
                MessageBox.Show("Customer table not found in DB.");
                return;
            }

            if (!int.TryParse(txtCustomerID.Text.Trim(), out int id))
            {
                MessageBox.Show("Customer ID must be a valid integer.");
                txtCustomerID.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        $"UPDATE [{customerTableName}] SET Customer_Name = @Customer_Name, Phone = @Phone, Email = @Email, Address = @Address, Username = @Username, Password = @Password " +
                        "WHERE Customer_ID = @Customer_ID", con);

                    cmd.Parameters.AddWithValue("@Customer_ID", id);
                    cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Record Updated Successfully");
                        ClearInputs();
                        LoadCustomerData();
                    }
                    else
                    {
                        MessageBox.Show("No record found with that Customer ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }

        // Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customerTableName))
            {
                MessageBox.Show("Customer table not found in DB.");
                return;
            }

            if (!int.TryParse(txtCustomerID.Text.Trim(), out int id))
            {
                MessageBox.Show("Customer ID must be a valid integer.");
                txtCustomerID.Focus();
                return;
            }

            var confirm = MessageBox.Show($"Delete customer ID {id}? This action cannot be undone.", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM [{customerTableName}] WHERE Customer_ID = @Customer_ID", con);
                    cmd.Parameters.AddWithValue("@Customer_ID", id);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully");
                        ClearInputs();
                        LoadCustomerData();
                    }
                    else
                    {
                        MessageBox.Show("No record found with that Customer ID.");
                    }
                }
            }
            catch (SqlException sqlex)
            {
                // common case: FK constraint prevents delete
                MessageBox.Show("Error deleting customer: " + sqlex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message);
            }
        }

        // Load clicked row into textboxes
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtCustomerID.Text = GetCellValueSafe(row, "Customer_ID", 0);
                txtCustomerName.Text = GetCellValueSafe(row, "Customer_Name", 1);
                txtPhoneNo.Text = GetCellValueSafe(row, "Phone", 2);
                txtEmail.Text = GetCellValueSafe(row, "Email", 3);
                txtAddress.Text = GetCellValueSafe(row, "Address", 4);
                txtUsername.Text = GetCellValueSafe(row, "Username", 5);
                txtPassword.Text = GetCellValueSafe(row, "Password", 6);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading selected row: " + ex.Message);
            }
        }

        // Helper: get column by name if present, else fallback to index
        private string GetCellValueSafe(DataGridViewRow row, string columnName, int fallbackIndex)
        {
            if (row == null) return string.Empty;

            if (dataGridView1.Columns.Contains(columnName))
            {
                var cell = row.Cells[columnName];
                return (cell?.Value == null) ? string.Empty : cell.Value.ToString();
            }
            else if (fallbackIndex >= 0 && fallbackIndex < row.Cells.Count)
            {
                var cell = row.Cells[fallbackIndex];
                return (cell?.Value == null) ? string.Empty : cell.Value.ToString();
            }
            return string.Empty;
        }

        private void ClearInputs()
        {
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtPhoneNo.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
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
