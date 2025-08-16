using Bank__Management_System;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class LoanRequest : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public LoanRequest()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Loans (Customer_ID, Loan_Type, Amount, Status) VALUES (@cid, @type, @amt, 'Pending')", con);
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                cmd.Parameters.AddWithValue("@type", txtLoanType.Text);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Loan request submitted.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }

        private void txtLoanType_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmitLoan_Click(object sender, EventArgs e)
        {

        }
    }
}
