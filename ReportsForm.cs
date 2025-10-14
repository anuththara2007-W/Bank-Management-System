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
            LoadTransactionsReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void LoadTransactionsReport()
        {
            try
            {
                string connectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";
                string query = "SELECT TID, Transaction_Type, Amount, Transaction_Date, Customer_ID, Purpose FROM transactions";

                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dt);
                }

                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // ✅ Your RDLC is directly in the project folder (not in a subfolder)
                string reportPath = System.IO.Path.Combine(Application.StartupPath, "Report1.rdlc");

                // Optional: Debug check
                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show("Report file not found at: " + reportPath);
                    return;
                }

                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
