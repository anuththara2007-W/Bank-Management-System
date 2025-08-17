using BankApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
                {
                    con.Open();

                    // Check if Account_ID exists
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM accounts WHERE Account_ID = @aid", con);
                    checkCmd.Parameters.AddWithValue("@aid", int.Parse(txtAccountID.Text));
                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists == 0)
                    {
                        MessageBox.Show("Account ID not found! Please create the account first.");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Transactions (TID, Transaction_Type, Amount, Transaction_Date, Account_ID) " +
                        "VALUES (@TID, @Transaction_Type, @Amount, @Transaction_Date, @Account_ID)", con);

                    cmd.Parameters.AddWithValue("@Transaction_Type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));

                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record saved successfully");
                    btnClear_Click(null, null);
                    LoadTransactions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message);
            }
        }

        private void LoadTransactions()
        {
            string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TID, Transaction_Type, Amount, Transaction_Date, Account_ID FROM Transactions", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.AutoGenerateColumns = true; // force showing all columns
                dataGridView1.DataSource = dt;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTransactionType.Clear();
            txtAmount.Clear();
            txtAccountID.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Transactions SET Transaction_Type = @Transaction_Type, Amount = @Amount, Transaction_Date = @Transaction_Date, Account_ID = @Account_ID WHERE TID = @TID",
                        con);

                    cmd.Parameters.AddWithValue("@Transaction_Type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));

                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record updated successfully");
                        LoadTransactions();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified Transaction ID.");
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM Transactions WHERE Customer_ID = @Customer_ID", con);

                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record deleted successfully");
                        LoadTransactions();
                        btnClear_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified Transaction ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Main admins = new Main();
            admins.Show();
            this.Hide();
        }

        private void btnPickCustomer_Click(object sender, EventArgs e)
        {
            using (var picker = new AccountPicker())
            {
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    txtAccountID.Text = picker.SelectedAccountID.ToString();
                }
            }
        }
    }
}
