using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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
            // 1. Empty field check
            if (string.IsNullOrWhiteSpace(txtOld.Text) ||
                string.IsNullOrWhiteSpace(txtNew.Text) ||
                string.IsNullOrWhiteSpace(txtConfirm.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. New password length check
            if (txtNew.Text.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Confirm password match check
            if (txtNew.Text != txtConfirm.Text)
            {
                MessageBox.Show("New password and confirm password do not match.", "Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // 4. Validate old password
                SqlCommand checkOld = new SqlCommand(
                    "SELECT COUNT(*) FROM Customers WHERE Customer_ID=@cid AND Password=@old", con);
                checkOld.Parameters.AddWithValue("@cid", Session.CustomerID);
                checkOld.Parameters.AddWithValue("@old", txtOld.Text);

                int count = (int)checkOld.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Old password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 5. Update password
                SqlCommand update = new SqlCommand(
                    "UPDATE Customers SET Password=@new WHERE Customer_ID=@cid", con);
                update.Parameters.AddWithValue("@new", txtNew.Text);
                update.Parameters.AddWithValue("@cid", Session.CustomerID);
                update.ExecuteNonQuery();

                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear textboxes after success
                txtOld.Clear();
                txtNew.Clear();
                txtConfirm.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            lblOld.BackColor = Color.Transparent;
            lblOld.BorderStyle = BorderStyle.None;
            lblneww.BackColor = Color.Transparent;
            lblneww.BorderStyle = BorderStyle.None;
            lblconfirm.BackColor = Color.Transparent;
            lblconfirm.BorderStyle = BorderStyle.None;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Landing Land = new Landing();
            Land.Show();
            this.Hide();
        }
    }
}
