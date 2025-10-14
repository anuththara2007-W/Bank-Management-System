using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Bank__Management_System
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadTransactionsReport()
        {
            // 🔹 1. Your SQL connection
            string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True";
            string query = "SELECT * FROM transactions";

            // 🔹 2. Fetch data from SQL
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
            }

            // 🔹 3. Set ReportViewer properties
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("BankDataSet_Transactions", dt);
            reportViewer1.LocalReport.DataSources.Add(rds);

            // 🔹 4. Link to RDLC file
            reportViewer1.LocalReport.ReportPath = @"Reports\TransactionsReport.rdlc";

            // 🔹 5. Refresh the report
            reportViewer1.RefreshReport();
        }
    }
}
