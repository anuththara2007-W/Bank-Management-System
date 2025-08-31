using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp
{
    public partial class AdminSupport : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public AdminSupport()
        {
            InitializeComponent();
        }

        private void AdminSupport_Load(object sender, EventArgs e)
        {
            LoadSupportTickets();
        }

        private void LoadSupportTickets()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string query = @"SELECT t.TicketID, c.FullName, c.Email, c.Phone, 
                                        t.Message, t.DateCreated, t.Status, t.Reply
                                 FROM SupportTickets t
                                 INNER JOIN Customers c ON t.Customer_ID = c.Customer_ID
                                 ORDER BY t.DateCreated DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSupport.DataSource = dt;
            }

            // Optional: nicer headers
            dgvSupport.Columns["TicketID"].HeaderText = "Ticket #";
            dgvSupport.Columns["FullName"].HeaderText = "Customer Name";
            dgvSupport.Columns["Email"].HeaderText = "Email";
            dgvSupport.Columns["Phone"].HeaderText = "Phone";
            dgvSupport.Columns["Message"].HeaderText = "Message";
            dgvSupport.Columns["DateCreated"].HeaderText = "Created On";
            dgvSupport.Columns["Status"].HeaderText = "Status";
            dgvSupport.Columns["Reply"].HeaderText = "Reply";
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            if (dgvSupport.SelectedRows.Count > 0)
            {
                int ticketId = Convert.ToInt32(dgvSupport.SelectedRows[0].Cells["TicketID"].Value);

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE SupportTickets SET Reply = @reply, Status = 'Answered' WHERE TicketID = @id", con);

                    cmd.Parameters.AddWithValue("@reply", txtReply.Text);
                    cmd.Parameters.AddWithValue("@id", ticketId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Reply sent.");
                LoadSupportTickets(); // refresh
            }
            else
            {
                MessageBox.Show("Select a ticket first.");
            }
        }
    }
}
