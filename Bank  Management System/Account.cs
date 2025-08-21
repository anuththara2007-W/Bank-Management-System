using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Account : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Account()
        {
            InitializeComponent();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Accounts", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtAccountType.Clear();
            txtBalance.Clear();
            txtname.Clear();
            txtCustomerID.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            txtAccountType.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE Customer_ID = @cid", con);
                    checkCmd.Parameters.AddWithValue("@cid", int.Parse(txtCustomerID.Text));
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0)
                    {
                        MessageBox.Show("Customer ID not found! Please create the customer first.");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Accounts (Account_Type, Balance, Date_Opened, Customer_Name, Customer_ID) " +
                        "VALUES (@account_type, @balance, @date_opened, @customer_name, @customer_id)", con);

                    cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text);
                    cmd.Parameters.AddWithValue("@balance", decimal.Parse(txtBalance.Text));
                    cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtname.Text);
                    cmd.Parameters.AddWithValue("@customer_id", int.Parse(txtCustomerID.Text));

                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record saved successfully");
                    btnAdd_Click(null, null);
                    LoadAccounts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Accounts SET Account_Type=@account_type, Balance=@balance, Date_Opened=@date_opened, Customer_Name=@customer_name " +
                        "WHERE Customer_ID=@customer_id", con);

                    cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text);
                    cmd.Parameters.AddWithValue("@balance", decimal.Parse(txtBalance.Text));
                    cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtname.Text);
                    cmd.Parameters.AddWithValue("@customer_id", int.Parse(txtCustomerID.Text));

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record updated successfully");
                        LoadAccounts();
                    }
                    else
                    {
                        MessageBox.Show("No record found for that Customer ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM Accounts WHERE Customer_ID=@customer_id", con);
                    cmd.Parameters.AddWithValue("@customer_id", int.Parse(txtCustomerID.Text));

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record deleted successfully");
                        LoadAccounts();
                    }
                    else
                    {
                        MessageBox.Show("No record found for that Customer ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Accounts WHERE Customer_Name=@name", con);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // Customer Picker Button - Shows popup with customers
        private void btnPickCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomerPicker();
        }

        private void ShowCustomerPicker()
        {
            // Create popup form
            Form popup = new Form();
            popup.Text = "Select Customer";
            popup.Size = new System.Drawing.Size(600, 400);
            popup.StartPosition = FormStartPosition.CenterParent;

            // Create DataGridView for customers
            DataGridView customerGrid = new DataGridView();
            customerGrid.Dock = DockStyle.Fill;
            customerGrid.ReadOnly = true;
            customerGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Load customers into popup grid
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string query = "SELECT Customer_ID, Customer_Name, phone, Email FROM Customers ORDER BY Customer_Name";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    customerGrid.DataSource = dt;
                }

                // Handle customer selection
                customerGrid.CellDoubleClick += (s, args) => {
                    if (args.RowIndex >= 0)
                    {
                        DataGridViewRow row = customerGrid.Rows[args.RowIndex];
                        txtCustomerID.Text = row.Cells["Customer_ID"].Value.ToString();
                        txtname.Text = row.Cells["Customer_Name"].Value.ToString();
                        popup.Close();
                    }
                };

                popup.Controls.Add(customerGrid);
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
        }
    }
}