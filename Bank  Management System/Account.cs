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
            string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM accounts", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // Clear all input fields
        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtAccountID.Clear();
            txtAccountType.Clear();
            txtBalance.Clear();
            txtname.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            txtAccountID.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO accounts (Account_ID, Account_Type, Balance, Date_Opened, Customer_Name) VALUES (@account_id, @account_type, @balance, @date_opened, @customer_name)", con);

                    cmd.Parameters.AddWithValue("@account_id", int.Parse(txtAccountID.Text));
                    cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text);
                    cmd.Parameters.AddWithValue("@balance", decimal.Parse(txtBalance.Text));
                    cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtname.Text);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record saved successfully");

                    // Clear inputs after saving
                    btnAdd_Click(null, null);

                    LoadAccounts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE accounts SET account_type = @account_type, balance = @balance, date_opened = @date_opened, customer_name = @customer_name WHERE account_id = @account_id",
                        con);

                    cmd.Parameters.AddWithValue("@account_id", int.Parse(txtAccountID.Text));
                    cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text);
                    cmd.Parameters.AddWithValue("@balance", decimal.Parse(txtBalance.Text));
                    cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtname.Text);

                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record updated successfully");
                        LoadAccounts();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified Account ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM accounts WHERE account_id = @account_id", con);
                    cmd.Parameters.AddWithValue("@account_id", int.Parse(txtAccountID.Text));

                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record deleted successfully");
                        LoadAccounts();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified Account ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM accounts WHERE customer_name = @customer_name", con);
                cmd.Parameters.AddWithValue("@customer_name", txtname.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                con.Close();
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
            Main admins = new Main();
            admins.Show();
            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;

            using (SqlConnection con = new SqlConnection(
                @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM accounts WHERE customer_name LIKE '%" + searchText + "%'", con);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

    }
}
