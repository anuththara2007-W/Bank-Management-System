using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class LoanRequest : Form
    {
        int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public LoanRequest(int cid)
        {
            InitializeComponent();
            customerId = cid;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0) { MessageBox.Show("Enter valid amount."); return; }
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Loan (Customer_ID, LoanType, Amount, InterestRate, LoanDate, Status) VALUES (@cid,@type,@amt,@ir,@date,@status)", con);

                    cmd.Parameters.AddWithValue("@cid", customerId);
                    cmd.Parameters.AddWithValue("@type", txtLoanType.Text);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@ir", decimal.TryParse(txtInterestRate.Text, out decimal ir) ? ir : 0m);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", "Pending");
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Loan request submitted.");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Error submitting loan: " + ex.Message); }
        }
    }
}
