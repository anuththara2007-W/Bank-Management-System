using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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

                    SqlCommand cmd = new SqlCommand("INSERT INTO Transactions  VALUES (@tid, @Transaction_Type, @Amount, @Transaction_Date, @Account_ID)", con);

                    cmd.Parameters.AddWithValue("@TID", int.Parse(txtTransactionID.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@Transaction_Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));

                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record saved successfully");

                    // Clear inputs after saving
                    btnSave_Click(null, null);

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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Transactions", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTransactionID.Clear();
            txtTransactionType.Clear();
            txtAmount.Clear();
            txtAccountID.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";


            txtAccountID.Focus();
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

                    cmd.Parameters.AddWithValue("@TID", int.Parse(txtTransactionID.Text));
                    cmd.Parameters.AddWithValue("@account_type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("@balance", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@date_opened", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtAccountID.Clear();
.Text);

                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record updated successfully");
                        LoadTransactions();
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
    }
    }

