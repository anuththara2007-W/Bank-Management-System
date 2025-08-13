using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class ChangePassword : Form
    {
        int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public ChangePassword(int cid)
        {
            InitializeComponent();
            customerId = cid;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtNew.Text != txtConfirm.Text) { MessageBox.Show("Passwords do not match."); return; }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Customer SET Password = @pw WHERE Customer_ID = @cid", con);
                cmd.Parameters.AddWithValue("@pw", txtNew.Text);
                cmd.Parameters.AddWithValue("@cid", customerId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Password changed.");
            this.Close();
        }
    }
}
