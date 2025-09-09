using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class Profile : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Profile()
        {
            InitializeComponent();

            // Hook up live update events
            txtName.TextChanged += TxtName_TextChanged;
            txtEmail.TextChanged += TxtEmail_TextChanged;
            txtPhone.TextChanged += TxtPhone_TextChanged;
            txtAddress.TextChanged += TxtAddress_TextChanged;
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT Customer_Name, Email, Phone, Address FROM Customers WHERE Customer_ID=@cid", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Load into textboxes
                    txtName.Text = reader["Customer_Name"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtAddress.Text = reader["Address"].ToString();

                    // Also load into labels
                    lblName.Text = txtName.Text;
                    lblEmail.Text = txtEmail.Text;
                    lblPhone.Text = txtPhone.Text;
                    lblAddress.Text = txtAddress.Text;
                }
            }
        }

        // Live update while typing
        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            lblName.Text = txtName.Text;
        }

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {
            lblEmail.Text = txtEmail.Text;
        }

        private void TxtPhone_TextChanged(object sender, EventArgs e)
        {
            lblPhone.Text = txtPhone.Text;
        }

        private void TxtAddress_TextChanged(object sender, EventArgs e)
        {
            lblAddress.Text = txtAddress.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customers SET Customer_Name=@name, Email=@mail, Phone=@phone, Address=@addr WHERE Customer_ID=@cid", con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@addr", txtAddress.Text);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);

                cmd.ExecuteNonQuery();

                // Update session (if you use it for display elsewhere)
                Session.CustomerName = txtName.Text;

                MessageBox.Show("Profile updated successfully.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }
    }
}
