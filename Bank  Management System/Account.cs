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
                cmd.Parameters.AddWithValue("@Date_Opened", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Customer_name", txtCustomerName.Text);


                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record saved Successfully");
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                dateTimePicker1.CustomFormat = " ";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

               SqlCommand cnn = new SqlCommand("SELECT * FROM accounts", con);
                SqlDataAdapter da = new SqlDataAdapter(cnn);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }

            MessageBox.Show("Record added Successfully");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customer SET Account_ID = @Account_ID, Account_Type = @Account_Type, Balance = @Balance, Address = @Address, Username = @Username, Password = @Password WHERE Customer_ID = @Customer_ID", con);

                cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));
                cmd.Parameters.AddWithValue("@Account_Type", txtAccountType.Text);
                cmd.Parameters.AddWithValue("@Balance", txtBalance.Text);
                cmd.Parameters.AddWithValue("@Date_Opened", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Customer_name", txtCustomerName.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Record Updated Successfully");
        }
    }
}
 