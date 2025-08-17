using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class LoanRequest : Form
    {
        // Use your existing connection string or DatabaseHelper if you prefer
        private readonly string connString =
            @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public LoanRequest()
        {
            InitializeComponent();
            this.Load += LoanRequest_Load;   // hook once
        }

        private void LoanRequest_Load(object sender, EventArgs e)
        {
            LoadLoanTypes();
            LoadMyRequests(); // <-- bind the customer's requests to the grid
        }

        private void LoadLoanTypes()
        {
            cmbLoanType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLoanType.Items.Clear();
            cmbLoanType.Items.AddRange(new object[]
            {
                "Personal Loan",
                "Home Loan",
                "Car Loan",
                "Education Loan",
                "Business Loan"
            });
            if (cmbLoanType.Items.Count > 0) cmbLoanType.SelectedIndex = 0;
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

            if (Session.CustomerID <= 0)
            {
                MessageBox.Show("Your session has expired. Please log in again.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(
                       @"INSERT INTO LoanRequests (Customer_ID, LoanType, Amount, Status, RequestDate)
                         VALUES (@cid, @type, @amt, 'Pending', GETDATE())", con))
            {
                cmd.Parameters.AddWithValue("@cid", Session.CustomerID);
                cmd.Parameters.AddWithValue("@type", cmbLoanType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@amt", amount);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Loan request submitted! Waiting for admin approval.");

                    // Clear UI and refresh list
                    txtAmount.Clear();
                    if (cmbLoanType.Items.Count > 0) cmbLoanType.SelectedIndex = 0;
                    LoadMyRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while submitting loan request: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Loads only the current customer's rows from LoanRequests
        /// and binds them to dgvLoanRequests.
        /// </summary>
        private void LoadMyRequests()
        {
            if (Session.CustomerID <= 0)
            {
                dgvLoanRequests.DataSource = null;
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlDataAdapter da = new SqlDataAdapter(
                       @"SELECT 
                             RequestID,
                             LoanType,
                             Amount,
                             Status,
                             RequestDate
                         FROM LoanRequests
                         WHERE Customer_ID = @cid
                         ORDER BY RequestDate DESC", con))
            {
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                try
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLoanRequests.AutoGenerateColumns = true;
                    dgvLoanRequests.DataSource = dt;
                    dgvLoanRequests.ReadOnly = true;
                    dgvLoanRequests.AllowUserToAddRows = false;
                    dgvLoanRequests.AllowUserToDeleteRows = false;
                    dgvLoanRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // optional formatting
                    if (dgvLoanRequests.Columns["Amount"] != null)
                        dgvLoanRequests.Columns["Amount"].DefaultCellStyle.Format = "N2";
                    if (dgvLoanRequests.Columns["RequestDate"] != null)
                        dgvLoanRequests.Columns["RequestDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading your loan requests: " + ex.Message);
                }
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            var customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }
    }
}
