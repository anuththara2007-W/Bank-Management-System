using Bank__Management_System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace BankApp
{
    public partial class ReportsForm : Form
    {
        private readonly string connString =
            @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public ReportsForm()
        {
            InitializeComponent();
            this.Load += ReportsForm_Load;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            cmbReportType.Items.Add("Transactions");
            cmbReportType.Items.Add("Loan Requests");
            cmbReportType.Items.Add("Deposits");
            cmbReportType.Items.Add("Withdrawals");
            cmbReportType.SelectedIndex = 0;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            string reportType = cmbReportType.SelectedItem.ToString();
            DataTable dt = GetReportData(reportType);
            dgvReport.DataSource = dt;
        }

        private DataTable GetReportData(string type)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "";

                if (type == "Transactions")
                    query = @"SELECT TID, Transaction_Type, Amount, Transaction_Date, Purpose 
                              FROM Transactions WHERE Customer_ID=@cid ORDER BY Transaction_Date DESC";
                else if (type == "Loan Requests")
                    query = @"SELECT RequestID, LoanType, Amount, Status, RequestDate 
                              FROM LoanRequests WHERE Customer_ID=@cid ORDER BY RequestDate DESC";
                else if (type == "Deposits")
                    query = @"SELECT TID, Amount, Transaction_Date 
                              FROM Transactions WHERE Customer_ID=@cid AND Transaction_Type='Deposit'";
                else if (type == "Withdrawals")
                    query = @"SELECT TID, Amount, Transaction_Date 
                              FROM Transactions WHERE Customer_ID=@cid AND Transaction_Type='Withdraw'";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
