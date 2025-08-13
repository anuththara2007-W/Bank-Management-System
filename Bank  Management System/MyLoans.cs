using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class MyLoans : Form
    {
        int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public MyLoans(int cid)
        {
            InitializeComponent();
            customerId = cid;
        }

        private void MyLoans_Load(object sender, EventArgs e)
        {
            LoadLoans();
        }

        private void LoadLoans()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string q = "SELECT LoanID, LoanType, Amount, InterestRate, LoanDate, Status FROM Loan WHERE Customer_ID = @cid";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.SelectCommand.Parameters.AddWithValue("@cid", customerId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLoans.AutoGenerateColumns = true;
                dgvLoans.DataSource = dt;
            }
        }
    }
}
