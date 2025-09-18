using Bank__Management_System;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class DepositWithdraw : Form
    {
        private int customerID = Session.CustomerID; // logged-in user
        private int accountID = 0;
        private string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public DepositWithdraw()
        {
            InitializeComponent();
            this.Load += DepositWithdraw_Load;
        }

        private void DepositWithdraw_Load(object sender, EventArgs e)
        {
            if (customerID <= 0)
            {
                MessageBox.Show("Please log in first!");
                this.Close();
                return;
            }

            LoadAccount();
            LoadTransactions();
        }

        // Load the first account and balance
        private void LoadAccount()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 Account_ID, Balance FROM Accounts WHERE Customer_ID=@cid", con);
                cmd.Parameters.AddWithValue("@cid", customerID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    accountID = (int)reader["Account_ID"];
                    decimal balance = (decimal)reader["Balance"];
                    lblBalance.Text = $"Account {accountID} Balance: {balance:C}";
                }
                reader.Close();
            }
        }

        // Load transactions into a grid
        private void LoadTransactions()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT TID, Transaction_Type, Amount, Transaction_Date FROM Transactions WHERE Account_ID=@aid ORDER BY Transaction_Date DESC",
                    con);
                da.SelectCommand.Parameters.AddWithValue("@aid", accountID);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvTransactions.DataSource = dt;
                dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Deposit money
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0) return;
            UpdateBalanceAndLog("Deposit", amt);
        }

        // Withdraw money
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0) return;
            UpdateBalanceAndLog("Withdraw", amt);
        }

        // Update account balance and log transaction
        private void UpdateBalanceAndLog(string type, decimal amount)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // Update balance
                SqlCommand cmd = new SqlCommand(
                    $"UPDATE Accounts SET Balance = Balance {(type == "Deposit" ? "+" : "-")} @amt WHERE Account_ID=@aid",
                    con);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.Parameters.AddWithValue("@aid", accountID);
                cmd.ExecuteNonQuery();

                // Log transaction
                SqlCommand log = new SqlCommand(
                    "INSERT INTO Transactions (Account_ID, Transaction_Type, Amount, Transaction_Date) VALUES (@aid,@type,@amt,GETDATE())",
                    con);
                log.Parameters.AddWithValue("@aid", accountID);
                log.Parameters.AddWithValue("@type", type);
                log.Parameters.AddWithValue("@amt", amount);
                log.ExecuteNonQuery();
            }

            LoadAccount();
            LoadTransactions();
            txtAmount.Clear();
            MessageBox.Show($"{type} successful!");
        }

        // Export transactions to PDF
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.DataSource == null || ((DataTable)dgvTransactions.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("No transactions to export!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Account_{accountID}_Statement.pdf"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            string filePath = sfd.FileName;
            GeneratePdf(filePath);
            MessageBox.Show("PDF generated: " + filePath);
        }

        // Simple PDF generation using QuestPDF
        private void GeneratePdf(string filePath)
        {
            var dt = (DataTable)dgvTransactions.DataSource;
            var title = $"Account {accountID} Statement";

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Header().Text("Bank Statement").FontSize(18).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        foreach (DataColumn c in dt.Columns) table.ColumnsDefinition(cd => cd.RelativeColumn());

                        table.Header(header =>
                        {
                            foreach (DataColumn c in dt.Columns)
                                header.Cell().Text(c.ColumnName).Bold();
                        });

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (var cell in row.ItemArray)
                                table.Cell().Text(cell?.ToString() ?? "");
                        }
                    });

                    page.Footer().AlignCenter().Text($"Generated {DateTime.Now}");
                });
            }).GeneratePdf(filePath);
        }
    }
}
