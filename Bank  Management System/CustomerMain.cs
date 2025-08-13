using System;
using System.Windows.Forms;

namespace BankApp
{
    public partial class CustomerMain : Form
    {
        public CustomerMain()
        {
            InitializeComponent();
        }

        private void CustomerMain_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = "Welcome, " + CustomerSession.CustomerName;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            new CustomerDashboard().Show();
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            new dgvAccounts().Show();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            new MyTransactions().Show();
        }

        private void btnDepositWithdraw_Click(object sender, EventArgs e)
        {
            new DepositWithdraw().Show();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            new TransferFunds().Show();
        }

        private void btnLoans_Click(object sender, EventArgs e)
        {
            new MyLoans().Show();
        }

        private void btnLoanRequest_Click(object sender, EventArgs e)
        {
            new LoanRequest().Show();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            new Profile().Show();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            new ChangePassword().Show();
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            new Support().Show();
        }
    }
}
