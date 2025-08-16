using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BankApp;

namespace Bank__Management_System
{
    public partial class LoanRequest : Form
    {
        public LoanRequest()
        {
            InitializeComponent();
        }

        private void btnSubmitLoan_Click(object sender, EventArgs e)
        {
            // ✅ Validate
            if (string.IsNullOrWhiteSpace(cmbLoanType.Text) ||
                string.IsNullOrWhiteSpace(txtAmount.Text) ||
                string.IsNullOrWhiteSpace(txtInterestRate.Text))
            {
                MessageBox.Show("Please fill in all fields before submitting.");
                return;
            }

            using (SqlConnection con = new SqlConnection(
                @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    con.Open();

                    string query = @"INSERT INTO Loan 
                                    (LoanType, Amount, InterestRate, LoanDate, CustomerName, Customer_ID) 
                                     VALUES (@type, @amt, @rate, @date, @cname, @cid)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    // ✅ Parameters
                    cmd.Parameters.AddWithValue("@type", cmbLoanType.Text);
                    cmd.Parameters.AddWithValue("@amt", Convert.ToDecimal(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@rate", Convert.ToDecimal(txtInterestRate.Text));
                    cmd.Parameters.AddWithValue("@date", dtpLoanDate.Value);

                    // ✅ Session Data from Login
                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                    cmd.Parameters.AddWithValue("@cname", Session.CustomerName);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        MessageBox.Show("✅ Loan request submitted successfully!");
                    else
                        MessageBox.Show("⚠ Loan request failed.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while submitting loan request: " + ex.Message);
                }
            }
        }
    }
}
