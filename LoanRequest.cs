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
            
        }

        private void LoanRequest_Load(object sender, EventArgs e)
        {
            LoadLoanTypes();
            LoadMyRequests(); // show my requests in grid
        }

        private void LoadLoanTypes()
        {
            cmbLoanType.DropDownStyle = ComboBoxStyle.DropDownList;
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

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
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

                    MessageBox.Show("✅ Loan request submitted! Waiting for admin approval.");

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

        /// <summary>
        /// Shows only this customer's requests from LoanRequests.
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

        
            public class TransparentLabel : Label
        {
            public TransparentLabel()
            {
                SetStyle(ControlStyles.Opaque, true);
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // Do nothing -> don't paint background
            }
        }

        
        private void LoanRequest_Load_1(object sender, EventArgs e)
        {
            lblAmount.BackColor = Color.Transparent;
            lblAmount.BorderStyle = BorderStyle.None;
            lblLoanType.BorderStyle = BorderStyle.None;
            lblLoanType.BackColor = Color.Transparent;
            ModernizeGrid(dgvLoanRequests);


        }
        private void ModernizeGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Rows
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.Black;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Alternate row shading
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // No row header
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 35;
            foreach (DataGridViewRow row in dgvLoanRequests.Rows)
            {
              
            }
        }




    }
}
