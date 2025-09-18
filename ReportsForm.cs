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
        private int customerID = Session.CustomerID; // logged in customer
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
                MessageBox.Show("Log in first!");
                this.Close();
                return;
            }

            LoadAccount();
            LoadTransactions();
        }

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
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0) return;
            UpdateBalanceAndLog("Deposit", amt);
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0) return;
            UpdateBalanceAndLog("Withdraw", amt);
        }

        private void UpdateBalanceAndLog(string type, decimal amount)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                // Update balance
                SqlCommand cmd = new SqlCommand(
                    $"UPDATE Accounts SET Balance = Balance {(type == "Deposit" ? "+" : "-")} @amt WHERE Account_ID=@aid", con);
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

        // Simple PDF export
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.DataSource == null) { MessageBox.Show("No transactions!"); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "PDF|*.pdf", FileName = "Statement.pdf" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var dt = (DataTable)dgvTransactions.DataSource;
            var filePath = sfd.FileName;

            QuestPDF.Fluent.Document.Create(container =>
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

            MessageBox.Show("PDF generated: " + filePath);
        }
    }
}
