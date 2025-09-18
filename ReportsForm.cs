using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.Web.WebView2.WinForms;
using Bank__Management_System;

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
            pdfViewer.EnsureCoreWebView2Async(null); // ✅ init WebView2
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("Transactions");
            cmbReportType.Items.Add("Deposits");
            cmbReportType.Items.Add("Withdrawals");
            cmbReportType.Items.Add("Loan Requests");
            cmbReportType.SelectedIndex = 0;
        }

        // ✅ Load report data into grid
        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem == null)
            {
                MessageBox.Show("Please select a report type.");
                return;
            }

            string type = cmbReportType.SelectedItem.ToString();
            DataTable dt = GetReportData(type);

            dgvPreview.DataSource = dt;
        }

        // ✅ Export PDF + Show inside WebView2
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (dgvPreview.DataSource == null || ((DataTable)dgvPreview.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("No data to export!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "Report.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd.FileName;

                GeneratePdf(filePath);

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("PDF generation failed!");
                    return;
                }

                // Show PDF in WebView2
                pdfViewer.Source = new Uri("file:///" + filePath.Replace("\\", "/"));

                // Optional: open explorer
                System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{filePath}\"");
            }
        }


        // ✅ Fetch DB data
        private DataTable GetReportData(string type)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "";

                if (type == "Transactions")
                    query = @"SELECT TID, Transaction_Type, Amount, Transaction_Date, Purpose 
                              FROM transactions WHERE Customer_ID=@cid ORDER BY Transaction_Date DESC";
                else if (type == "Loan Requests")
                    query = @"SELECT RequestID, LoanType, Amount, Status, RequestDate 
                              FROM LoanRequests WHERE Customer_ID=@cid ORDER BY RequestDate DESC";
                else if (type == "Deposits")
                    query = @"SELECT TID, Amount, Transaction_Date 
                              FROM transactions WHERE Customer_ID=@cid AND Transaction_Type='Deposit'";
                else if (type == "Withdrawals")
                    query = @"SELECT TID, Amount, Transaction_Date 
                              FROM transactions WHERE Customer_ID=@cid AND Transaction_Type='Withdraw'";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ✅ Generate modern PDF
        private void GeneratePdf(string filePath)
        {
            var dt = dgvPreview.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0) return;

            var title = cmbReportType.SelectedItem.ToString() + " Report";

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Header()
                        .Text("Bank Of Codes")
                        .FontSize(20)
                        .Bold().AlignCenter();

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text(title).FontSize(16).Bold().Underline().AlignCenter();
                        col.Item().PaddingVertical(10);

                        // ✅ Table
                        col.Item().Table(table =>
                        {
                            // Columns
                            foreach (DataColumn c in dt.Columns)
                                table.ColumnsDefinition(cd => cd.RelativeColumn());

                            // Header
                            table.Header(header =>
                            {
                                foreach (DataColumn c in dt.Columns)
                                    header.Cell().Element(CellStyle)
                                        .Text(c.ColumnName).Bold().FontSize(12);
                            });

                            // Rows
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (var cell in row.ItemArray)
                                    table.Cell().Element(CellStyle)
                                        .Text(cell?.ToString() ?? "");
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated on {DateTime.Now:yyyy-MM-dd HH:mm}");
                });
            }).GeneratePdf(filePath);
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container.BorderBottom(1).BorderColor("#DDD")
                     .PaddingVertical(5).PaddingHorizontal(2);
        }
    }
}
