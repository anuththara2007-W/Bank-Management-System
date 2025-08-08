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
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM accounts", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                 
                SqlCommand cmd = new SqlCommand("INSERT INTO accounts (account_id, account_type, balance, date_opened, customer_name) VALUES (@account_id, @account_type, @balance, @date_opened, @customer_name)\r\n)", con);

                cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));
                cmd.Parameters.AddWithValue("@Account_Type", txtAccountType.Text);
                cmd.Parameters.AddWithValue("@Balance", txtBalance.Text);
                cmd.Parameters.AddWithValue("@Date_Opened", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Customer_name", txtCustomerName.Text);


                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record saved Successfully");
                LoadAccounts();
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
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False");
            
                con.Open();

               SqlCommand cnn = new SqlCommand("SELECT * FROM accounts", con);
                SqlDataAdapter da = new SqlDataAdapter(cnn);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            

            MessageBox.Show("Record added Successfully");
            LoadAccounts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE accounts SET Account_Type = @Account_Type, Balance = @Balance, Date_Opened = @Date_Opened, Customer_name = @Customer_name where account_id = @account_id", con);

                cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));
                cmd.Parameters.AddWithValue("@Account_Type", txtAccountType.Text);
                cmd.Parameters.AddWithValue("@Balance", txtBalance.Text);
                cmd.Parameters.AddWithValue("@Date_Opened", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Customer_name", txtCustomerName.Text);

                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("Record Updated Successfully");
            LoadAccounts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM  accounts WHERE account_id = @account_id", con);
                cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));

                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("Record Deleted Successfully");
            LoadAccounts();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False");
             con.Open();
            SqlCommand cnn = new SqlCommand("SELECT * FROM accounts where customer_name= @customer_name", con);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main admin = new Main();
            admin.Show();
        }
    }
}
 