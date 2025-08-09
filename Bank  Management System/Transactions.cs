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

                    SqlCommand cmd = new SqlCommand("INSERT INTO Transactions (txtTransactionID, txtTransactionType, txtAmount, txtAccountID) VALUES (@account_id, @account_type, @balance, @date_opened, @customer_name)", con);

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

                    LoadTransactions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message);
            }
        }
    }
    }

