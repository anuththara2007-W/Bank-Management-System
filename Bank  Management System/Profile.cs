using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bank__Management_System
{
    public partial class Profile : Form
    {
        int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Profile(int cid)
        {
            InitializeComponent();
            customerId = cid;
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Customer_Name, Phone, Email, Address, Username FROM Customer WHERE Customer_ID = @cid", con);
                cmd.Parameters.AddWithValue("@cid", customerId);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        txtName.Text = r["Customer_Name"]?.ToString();
                        txtPhone.Text = r["Phone"]?.ToString();
                        txtEmail.Text = r["Email"]?.ToString();
                        txtAddress.Text = r["Address"]?.ToString();
                        txtUsername.Text = r["Username"]?.ToString();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Customer SET Customer_Name=@nm, Phone=@ph, Email=@em, Address=@ad WHERE Customer_ID=@cid", con);
                cmd.Parameters.AddWithValue("@nm", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@ph", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@em", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@ad", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@cid", customerId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Profile updated.");
            this.Close();
        }
    }
}
