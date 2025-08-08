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
    public partial class txtDateOpened : Form
    {
        public txtDateOpened()
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

                cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));
                cmd.Parameters.AddWithValue("@Account_Type", txtAccountType.Text);
                cmd.Parameters.AddWithValue("@Balance", txtBalance.Text);
                cmd.Parameters.AddWithValue("@Date_Opened", txtDateOpened.Text);
                cmd.Parameters.AddWithValue("@Customer_name", txtCustomerName.Text);


                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Account Created Successfully");
            }

        }
    }
}
 