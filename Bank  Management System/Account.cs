using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Account : Form
    {
        private string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Account()
        {
            InitializeComponent();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            dateTimePicker1.CustomFormat = "dd/MM/yyyy"; // set default format
        }

        private void LoadAccounts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM accounts", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int accountId, out decimal balance)) return;

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                string sql = "INSERT INTO accounts (Account_ID, Account_Type, Balance, Date_Opened, Customer_Name) " +
                             "VALUES (@account_id, @account_type, @balance, @date_opened, @customer_name)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text.Trim());
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtname.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Record saved successfully.");
                        LoadAccounts();
                    }
                    else
                    {
                        MessageBox.Show("Save failed.");
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int accountId, out decimal balance)) return;

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                string sql = "UPDATE accounts SET account_type = @account_type, balance = @balance, date_opened = @date_opened, customer_name = @customer_name WHERE account_id = @account_id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    cmd.Parameters.AddWithValue("@account_type", txtAccountType.Text.Trim());
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtname.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                        LoadAccounts();
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Check Account ID.");
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAccountID.Text, out int accountId))
            {
                MessageBox.Show("Invalid Account ID for deletion.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM accounts WHERE account_id = @account_id", con))
                {
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Record deleted successfully.");
                        LoadAccounts();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed. Check Account ID.");
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string customerName = txtname.Text.Trim();
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter a Customer Name to search.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM accounts WHERE customer_name LIKE @customer_name", con))
                {
                    cmd.Parameters.AddWithValue("@customer_name", "%" + customerName + "%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                dateTimePicker1.CustomFormat = " ";
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Main admin = new Main();
            admin.Show();
            this.Close();
        }

       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Clear all input fields
            txtAccountID.Clear();
            txtAccountType.Clear();
            txtBalance.Clear();
            txtname.Clear();

            // Reset the date picker to today or default
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            // Optionally, set focus to the first field for convenience
            txtAccountID.Focus();
        }
    }
}
