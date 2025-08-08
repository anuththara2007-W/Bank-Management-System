using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bank__Management_System
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }

        private void Account_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                 
                SqlCommand cmd = new SqlCommand("INSERT INTO accounts values(@account_id, @account_type, @balance, @date_opened, @customer_name)", con);

                cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtCustomerID.Text));
                cmd.Parameters.AddWithValue("@Account_Type", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@Balance", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Date_Opened", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Customer_name", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Saved");
            LoadCustomerData();
        }
    }
}
 