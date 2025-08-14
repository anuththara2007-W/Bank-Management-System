using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class ChangePassword : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtNew.Text != txtConfirm.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand checkOld = new SqlCommand(
                    "SELECT COUNT(*) FROM Customer WHERE Customer_ID=@cid AND Password=@old", con);
                checkOld.Parameters.AddWithValue("@cid", Session.CustomerID);
                checkOld.Parameters.AddWithValue("@old", txtOld.Text);
                if ((int)checkOld.ExecuteScalar() == 0)
                {
                    MessageBox.Show("Old password incorrect");
                    return;
                }

                SqlCommand update = new SqlCommand(
                    "UPDATE Customer SET Password=@new WHERE Customer_ID=@cid", con);
                update.Parameters.AddWithValue("@new", txtNew.Text);
                update.Parameters.AddWithValue("@cid", Session.CustomerID);
                update.ExecuteNonQuery();
                MessageBox.Show("Password changed.");
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
