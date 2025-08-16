using Bank__Management_System;
using System;
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
            LoadLoanTypes(); // Load loan types into combo box
        }

        private void LoadLoanTypes()
        {
            // Predefined loan types - you can also load from DB if needed
            cmbLoanType.Items.Clear();
            cmbLoanType.Items.Add("Personal Loan");
            cmbLoanType.Items.Add("Home Loan");
            cmbLoanType.Items.Add("Car Loan");
            cmbLoanType.Items.Add("Education Loan");
            cmbLoanType.Items.Add("Business Loan");

            cmbLoanType.SelectedIndex = 0; // Default selection
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
     "INSERT INTO Loan (Customer_ID, CustomerName, LoanType, Amount, InterestRate, LoanDate) " +
     "VALUES (Customer_ID, CustomerName, LoanType, Amount, InterestRate, GETDATE())", con);

                    cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                    cmd.Parameters.AddWithValue("@cname", Session.CustomerName);
                    cmd.Parameters.AddWithValue("@type", cmbLoanType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@rate", 5.5m);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Loan request submitted successfully!");
                    txtAmount.Clear();
                    cmbLoanType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while submitting loan request: " + ex.Message);
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
