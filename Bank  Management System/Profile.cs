using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class Profile : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Customer_Name, Email, Phone FROM Customer WHERE Customer_ID=@cid", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["Customer_Name"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customer SET Customer_Name=@name, Email=@mail, Phone=@phone WHERE Customer_ID=@cid", con);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                cmd.ExecuteNonQuery();
                Session.CustomerName = txtName.Text;
                MessageBox.Show("Profile updated.");
            }
        }
    }
}
