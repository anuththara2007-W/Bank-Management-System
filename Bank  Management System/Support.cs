using Bank__Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BankApp
{
    public partial class Support : Form
    {
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        private void Support_Load(object sender, EventArgs e)
        {
            LoadMyTickets();
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

            // optional: nice headers
            dgvMyTickets.Columns["TicketID"].HeaderText = "Ticket #";
            dgvMyTickets.Columns["Message"].HeaderText = "Message";
            dgvMyTickets.Columns["DateCreated"].HeaderText = "Created On";
            dgvMyTickets.Columns["Status"].HeaderText = "Status";
            dgvMyTickets.Columns["Reply"].HeaderText = "Reply";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CustomerDashboard customerdash = new CustomerDashboard();
            customerdash.Show();
            this.Hide();
        }
    }
}
