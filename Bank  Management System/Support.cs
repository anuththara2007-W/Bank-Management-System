using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class Support : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Support()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Enter a message");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO SupportTickets (Customer_ID, Message, DateCreated, Status) VALUES (@cid, @msg, GETDATE(), 'Open')", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                cmd.Parameters.AddWithValue("@msg", txtMessage.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Message sent to support.");
                txtMessage.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login gonow = new Login();
            gonow.Show();
            this.Hide();
        }
    }
}
