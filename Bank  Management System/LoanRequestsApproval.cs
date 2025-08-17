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

namespace Bank__Management_System
{
        public partial class LoanRequestsApproval : Form
        {
            string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

            public LoanRequestsApproval()
            {
                InitializeComponent();
                LoadRequests();
            }

            private void LoadRequests()
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM LoanRequests WHERE Status='Pending'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRequests.DataSource = dt; // Show in DataGridView
                }
            }

            private void btnApprove_Click(object sender, EventArgs e)
            {
                if (dgvRequests.SelectedRows.Count > 0)
                {
                    int requestId = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["RequestID"].Value);
                    int customerId = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["Customer_ID"].Value);
                    string loanType = dgvRequests.SelectedRows[0].Cells["LoanType"].Value.ToString();
                    decimal amount = Convert.ToDecimal(dgvRequests.SelectedRows[0].Cells["Amount"].Value);

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        con.Open();

                        // Insert into Loans table
                        SqlCommand insertLoan = new SqlCommand(
                            "INSERT INTO Loans (Customer_ID, LoanType, Amount, InterestRate, LoanDate) " +
                            "VALUES (@cid, @type, @amt, @rate, GETDATE())", con);

                        insertLoan.Parameters.AddWithValue("@cid", customerId);
                        insertLoan.Parameters.AddWithValue("@type", loanType);
                        insertLoan.Parameters.AddWithValue("@amt", amount);
                        insertLoan.Parameters.AddWithValue("@rate", 5.5m);

                        insertLoan.ExecuteNonQuery();

                        // Update request status
                        SqlCommand updateRequest = new SqlCommand(
                            "UPDATE LoanRequests SET Status='Approved' WHERE RequestID=@rid", con);
                        updateRequest.Parameters.AddWithValue("@rid", requestId);
                        updateRequest.ExecuteNonQuery();

                        MessageBox.Show("✅ Loan approved and added to Loans table!");
                        LoadRequests();
                    }
                }
            }

            private void btnReject_Click(object sender, EventArgs e)
            {
                if (dgvRequests.SelectedRows.Count > 0)
                {
                    int requestId = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["RequestID"].Value);

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE LoanRequests SET Status='Rejected' WHERE RequestID=@rid", con);
                        cmd.Parameters.AddWithValue("@rid", requestId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("❌ Loan request rejected.");
                        LoadRequests();
                    }
                }
            }
        }
    }
