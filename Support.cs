using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class Support : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Support()
        {
            InitializeComponent();
        }

        private void Support_Load(object sender, EventArgs e)
        {
            LoadMyTickets(); // load history when form opens
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Enter a message");
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO SupportTickets (Customer_ID, Message, DateCreated, Status, Reply) " +
                    "VALUES (@cid, @msg, GETDATE(), 'Open', 'We have received your message. Our team will contact you.')", con);

                cmd.Parameters.AddWithValue("@cid", Session.CustomerID); // logged in customer
                cmd.Parameters.AddWithValue("@msg", txtMessage.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Message sent to support.");
            txtMessage.Clear();
            LoadMyTickets(); // refresh grid
        }

        private void LoadMyTickets()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT TicketID, Message, DateCreated, Status, Reply " +
                    "FROM SupportTickets WHERE Customer_ID = @cid ORDER BY DateCreated DESC", con);

                da.SelectCommand.Parameters.AddWithValue("@cid", Session.CustomerID);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMyTickets.DataSource = dt;
            }

            // Optional: nicer headers
            dgvMyTickets.Columns["TicketID"].HeaderText = "Ticket #";
            dgvMyTickets.Columns["Message"].HeaderText = "Message";
            dgvMyTickets.Columns["DateCreated"].HeaderText = "Created On";
            dgvMyTickets.Columns["Status"].HeaderText = "Status";
            dgvMyTickets.Columns["Reply"].HeaderText = "Reply";
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }

        private void Support_Load_1(object sender, EventArgs e)
        {
            dgvMyTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridStyle.ModernizeGrid(dgvMyTickets);
            lblLoanType.BorderStyle = BorderStyle.None;
            lblLoanType.BackColor = Color.Transparent;
        }
    }
}
