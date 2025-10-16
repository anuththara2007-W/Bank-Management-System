
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
                string connectionString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

                //get the cus id from the session
                int custID = Session.CustomerID;

                string query = "SELECT * FROM transactions WHERE Customer_ID = @custID ORDER BY Transaction_Date DESC";

                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, con);

                    // Add the parameter to the query to safely pass the customer ID
                    da.SelectCommand.Parameters.AddWithValue("@custID", custID);

                    da.Fill(dt);
                }

                // Clear any old data sources from the ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();

                // Create a new ReportDataSource using the DataTable
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1"; 
                rds.Value = dt;

                // Add the new data source to the ReportViewer
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Set the path of the RDLC report file
                string reportPath = System.IO.Path.Combine(Application.StartupPath, "Report1.rdlc");
                reportViewer1.LocalReport.ReportPath = reportPath;

                // Refresh the ReportViewer to show the updated data
                reportViewer1.RefreshReport();



            }
            catch (Exception ex)
                {
                    MessageBox.Show("Error loading report:\n" + ex.Message);
                }
            }
        }
    }
