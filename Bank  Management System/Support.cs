using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Support : Form
    {
        int customerId;
        string connString = @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public Support(int cid)
        {
            InitializeComponent();
            customerId = cid;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text) || string.IsNullOrWhiteSpace(txtMessage.Text)) { MessageBox.Show("Enter subject and message."); return; }

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO SupportRequests (Customer_ID, Subject, Message, RequestDate, Status) VALUES (@cid,@sub,@msg,@dt,@st)", con);
                cmd.Parameters.AddWithValue("@cid", customerId);
                cmd.Parameters.AddWithValue("@sub", txtSubject.Text.Trim());
                cmd.Parameters.AddWithValue("@msg", txtMessage.Text.Trim());
                cmd.Parameters.AddWithValue("@dt", DateTime.Now);
                cmd.Parameters.AddWithValue("@st", "Open");
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Support request sent.");
            this.Close();
        }
    }
}
