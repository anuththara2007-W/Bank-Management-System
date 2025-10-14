
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
                    // 🔹 Your real connection string
                    string connectionString = @"Data Source = (localdb)\Local; Initial Catalog = BankDB; Integrated Security = True; Encrypt = False";
                    int custID = Session.CustomerID;

                    // 🔹 Always pull the latest data
                    string query = "SELECT * FROM transactions WHERE Customer_ID = @custID ORDER BY Transaction_Date DESC";

                    DataTable dt = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(query, con);
                        da.SelectCommand.Parameters.AddWithValue("@custID", custID);
                        da.Fill(dt);
                    }

                    // 🔹 Clear old data and load new
                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    reportViewer1.LocalReport.DataSources.Add(rds);

                    string reportPath = System.IO.Path.Combine(Application.StartupPath, "Report1.rdlc");
                    reportViewer1.LocalReport.ReportPath = reportPath;

                    // 🔹 This forces the viewer to reload the new dataset
                    reportViewer1.LocalReport.Refresh();
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading report:\n" + ex.Message);
                }
            }
        }
    }
