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

        // =========================
        // Save / Add Customer
        // =========================
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

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
            }

            MessageBox.Show("✅ Customer Saved Successfully");
            LoadCustomerData();
            ClearFields();
        }

        private void ClearFields()
        {
            throw new NotImplementedException();
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
    }

        // =========================
        // Update Customer
        // =============
