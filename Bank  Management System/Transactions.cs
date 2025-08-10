using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Transactions : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

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
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Transactions (TID, Transaction_Type, Amount, Transaction_Date, Account_ID) " +
                        "VALUES (@TID, @Transaction_Type, @Amount, @Transaction_Date, @Account_ID)", con);

                    cmd.Parameters.AddWithValue("@TID", int.Parse(txtTransactionID.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record saved successfully");

                    ClearFields();
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
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Transactions", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtTransactionID.Clear();
            txtTransactionType.Clear();
            txtAmount.Clear();
            txtAccountID.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            txtTransactionID.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Transactions SET Transaction_Type = @Transaction_Type, Amount = @Amount, " +
                        "Transaction_Date = @Transaction_Date, Account_ID = @Account_ID WHERE TID = @TID", con);

                    cmd.Parameters.AddWithValue("@TID", int.Parse(txtTransactionID.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));

                    int rows = cmd.ExecuteNonQuery();

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
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Transactions WHERE TID = @TID", con);
                    cmd.Parameters.AddWithValue("@TID", int.Parse(txtTransactionID.Text));

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record deleted successfully");
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
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
        }
    }
}
