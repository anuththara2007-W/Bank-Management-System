using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bank__Management_System
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();

            // Hook the DataGridView CellClick event here

        }

        // Save / Add
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Customer (Customer_ID, Customer_Name, Phone, Email, Address, Username, Password) " +
                    "VALUES (@Customer_ID, @Customer_Name, @Phone, @Email, @Address, @Username, @Password)", con);

                cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Saved");
            LoadCustomerData();
        }

        // Load / Refresh
        private void LoadCustomerData()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        // Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customer SET Customer_Name = @Customer_Name, Phone = @Phone, Email = @Email, Address = @Address, Username = @Username, Password = @Password WHERE Customer_ID = @Customer_ID", con);

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
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.ExecuteNonQuery();
             
            }

            MessageBox.Show("Record Deleted Successfully");
            LoadCustomerData();
        }

        // Form Load
        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        // Optional: You can link this to your "Load" or "Refresh" button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Main admin = new Main();
            admin.Show();
            this.Hide();
        }

        // New: DataGridView CellClick to load selected row data into form fields for update/delete

    }
}
