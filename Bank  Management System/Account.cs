using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                SqlCommand cmd = new SqlCommand("INSERT INTO accounts (Account_ID, Account_Type, Balance, Date_Opened, Customer_Name) VALUES (@Account_ID, @Account_Type, @Balance, @Date_Opened, @Customer_Name)", con);

                cmd.Parameters.AddWithValue("@account_id", int.Parse(txtAccountID.Text));
                cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text);
                cmd.Parameters.AddWithValue("@balance", txtBalance.Text);
                cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@customer_name", txtCustomerName.Text);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Record saved successfully");
            LoadAccounts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE accounts SET account_type = @account_type, balance = @balance, date_opened = @date_opened, customer_name = @customer_name WHERE account_id = @account_id", con);

                cmd.Parameters.AddWithValue("@account_id", int.Parse(txtAccountID.Text));
                cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text);
                cmd.Parameters.AddWithValue("@balance", txtBalance.Text);
                cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@customer_name", txtCustomerName.Text);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Record updated successfully");
            LoadAccounts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM accounts WHERE account_id = @account_id", con);
                cmd.Parameters.AddWithValue("@account_id", int.Parse(txtAccountID.Text));

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Record deleted successfully");
            LoadAccounts();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM accounts WHERE customer_name = @customer_name", con);
                cmd.Parameters.AddWithValue("@customer_name", txtCustomerName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
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

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Main admin = new Main();
            admin.Show();
            this.Close();
        }
    }
}
