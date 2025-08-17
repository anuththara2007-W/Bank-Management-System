using Bank__Management_System;
using BankApp;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Customer_Click(object sender, EventArgs e)
        {
            Customer cr = new Customer();
            cr.Show();
            this.Hide();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            Account acc = new Account();
            acc.Show();
            this.Hide();
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            Transactions acc = new Transactions();
            acc.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
           Dashboard dash = new Dashboard();
            dash.Show();
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Show();
            this.Hide();
        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            Loan ln = new Loan();
            ln.Show();
            this.Hide();
        }

        private void btnLoanRq_Click(object sender, EventArgs e)
        {
            LoanRequestsApproval ln = new LoanRequestsApproval();
            ln.Show();
            this.Hide();
        }
    }
}
