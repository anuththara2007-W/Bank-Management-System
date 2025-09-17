using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BankApp
{
    public partial class LoanRequest : Form
    {
        private readonly string connString =
            @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public LoanRequest()
        {
            InitializeComponent();
            this.Load += LoanRequest_Load; // Load form event
        }

        private void LoanRequest_Load(object sender, EventArgs e)
        {
            LoadLoanTypes();
            LoadMyRequests(); // show my requests in grid
            lblAmount.BackColor = Color.Transparent;
            lblAmount.BorderStyle = BorderStyle.None;
            lblLoanType.BorderStyle = BorderStyle.None;
            lblLoanType.BackColor = Color.Transparent;
        }

        private void LoadLoanTypes()
        {
          
            cmbLoanType.Items.Clear();
            cmbLoanType.Items.Add("Personal Loan");
            cmbLoanType.Items.Add("Home Loan");
            cmbLoanType.Items.Add("Car Loan");
            cmbLoanType.Items.Add("Education Loan");
            cmbLoanType.Items.Add("Business Loan");

            if (cmbLoanType.Items.Count > 0)
                cmbLoanType.SelectedIndex = 0;
        }

        private void btnSubmitLoan_Click(object sender, EventArgs e)
        {
            if (cmbLoanType.SelectedItem == null)
            {
                MessageBox.Show("Please select a loan type.");
                return;
            }

            decimal amount = 0;
            bool isValid = decimal.TryParse(txtAmount.Text, out amount);

            if (!isValid || amount <= 0)
            {
                MessageBox.Show("Enter a valid loan amount.");
                return;
            }

            if (Session.CustomerID <= 0)
            {
                MessageBox.Show("Session expired. Please log in again.");
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

                    MessageBox.Show("Loan request submitted! Waiting for admin approval.");

                    txtAmount.Clear();
                    if (cmbLoanType.Items.Count > 0) cmbLoanType.SelectedIndex = 0;

                    LoadMyRequests(); // Refresh grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while submitting loan request: " + ex.Message);
                }
            }
        }

       
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
                    // Load data into the grid
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Show data in the grid
                    dgvLoanRequests.DataSource = dt;
                    dgvLoanRequests.ReadOnly = true;
                    dgvLoanRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Format Amount and RequestDate columns if they exist
                    if (dgvLoanRequests.Columns["Amount"] != null)
                        dgvLoanRequests.Columns["Amount"].DefaultCellStyle.Format = "N2";

                    if (dgvLoanRequests.Columns["RequestDate"] != null)
                        dgvLoanRequests.Columns["RequestDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading loan requests: " + ex.Message);
                }
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            var customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }

        
      

       

        private void LoanRequest_Load_1(object sender, EventArgs e)
        {
            dgvLoanRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            object value = GridStyle.ModernizeGrid(dgvLoanRequests);

        }
    }
}
