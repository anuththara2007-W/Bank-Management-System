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
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                // 💡 Change this to your real server name & database name
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BankDB;Integrated Security=True;";

                // 💡 Use the customer ID from your Session
                int custID = Session.CustomerID;

                // 💡 Get only this customer’s transactions
                string query = "SELECT * FROM transactions WHERE Customer_ID = @custID";

                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@custID", custID);
                    da.Fill(dt);
                }

                // 💡 Connect report
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSet1", dt)
                );

                // 💡 Report file path
                string reportPath = System.IO.Path.Combine(Application.StartupPath, "Report1.rdlc");
                reportViewer1.LocalReport.ReportPath = reportPath;

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
