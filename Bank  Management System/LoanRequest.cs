using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class LoanRequest : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public LoanRequest()
        {
            InitializeComponent();
            LoadLoanTypes();

            // Load existing loans when form opens
            LoadMyLoans();
        }

        private void LoadLoanTypes()
        {
            cmbLoanType.Items.Clear();
            cmbLoanType.Items.Add("Personal Loan");
            cmbLoanType.Items.Add("Home Loan");
            cmbLoanType.Items.Add("Car Loan");
            cmbLoanType.Items.Add("Education Loan");
            cmbLoanType.Items.Add("Business Loan");

            cmbLoanType.SelectedIndex = 0;
        }

        private void btnSubmitLoan_Click(object sender, EventArgs e)
        {
            if (cmbLoanType.SelectedItem == null)
            {
                MessageBox.Show("Please select a loan type.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid loan amount.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO LoanRequests (Customer_ID, LoanType, Amount, Status) " +
                        "VALUES (@cid, @type, @amt, 'Pending')", con);

                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                    cmd.Parameters.AddWithValue("@type", cmbLoanType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@amt", amount);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Loan request submitted! Waiting for admin approval.");
                    txtAmount.Clear();
                    cmbLoanType.SelectedIndex = 0;

                    // Refresh grid after submitting
                    LoadMyLoans();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while submitting loan request: " + ex.Message);
                }
            }
        }

        private void LoadMyLoans()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                try
                {
                    con.Open();

                    // Fetch both approved and pending requests for the customer
                    SqlDataAdapter da = new SqlDataAdapter(
                        @"SELECT LoanID, LoanType, Amount, Status, LoanDate, InterestRate 
                          FROM Loans 
                          WHERE Customer_ID = @cid
                          UNION
                          SELECT RequestID AS LoanID, LoanType, Amount, Status, RequestDate AS LoanDate, NULL AS InterestRate 
                          FROM LoanRequests 
                          WHERE Customer_ID = @cid", con);

                    da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLoans.DataSource = dt; // Make sure dgvLoans exists on your form
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading loans: " + ex.Message);
                }
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }
    }
}
