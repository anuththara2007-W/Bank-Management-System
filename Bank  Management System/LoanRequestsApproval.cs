using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class AdminLoanRequests : Form
    {
        private readonly string connString =
            @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public AdminLoanRequests()
        {
            InitializeComponent();
            LoadRequests();
        }

        
        private void LoadRequests()
        {
            using (SqlConnection con = new SqlConnection(connString))
            using (SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT 
                     RequestID,
                     Customer_ID,
                     LoanType,
                     Amount,
                     Status,
                     RequestDate
                  FROM LoanRequests
                  ORDER BY RequestDate DESC", con))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAllRequests.DataSource = dt;
                dgvAllRequests.ReadOnly = true;
                dgvAllRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvAllRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request to approve.");
                return;
            }

            int requestId = Convert.ToInt32(dgvAllRequests.SelectedRows[0].Cells["RequestID"].Value);

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE LoanRequests SET Status = 'Approved' WHERE RequestID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", requestId);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Loan request approved.");
                    LoadRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving loan: " + ex.Message);
                }
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvAllRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request to reject.");
                return;
            }

            int requestId = Convert.ToInt32(dgvAllRequests.SelectedRows[0].Cells["RequestID"].Value);

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE LoanRequests SET Status = 'Rejected' WHERE RequestID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", requestId);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("❌ Loan request rejected.");
                    LoadRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error rejecting loan: " + ex.Message);
                }
            }
        }
    }
}
