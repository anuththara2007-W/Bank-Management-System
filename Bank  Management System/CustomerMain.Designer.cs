namespace Bank__Management_System
{
    partial class CustomerMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnAccounts;
        private System.Windows.Forms.Button btnTransactions;
        private System.Windows.Forms.Button btnDepositWithdraw;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnLoans;
        private System.Windows.Forms.Button btnLoanRequest;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnSupport;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnAccounts = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.btnDepositWithdraw = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnLoans = new System.Windows.Forms.Button();
            this.btnLoanRequest = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblCustomerName
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(20, 20);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(120, 20);
            this.lblCustomerName.Text = "Welcome, User";

            // Buttons setup (just example for one, repeat for all)
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.Location = new System.Drawing.Point(20, 60);
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);

            this.btnAccounts.Text = "My Accounts";
            this.btnAccounts.Location = new System.Drawing.Point(20, 100);
            this.btnAccounts.Click += new System.EventHandler(this.btnAccounts_Click);

            // Add more buttons here in similar way...

            // CustomerMain Form
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.btnDashboard);
            this.Controls.Add(this.btnAccounts);
            this.Controls.Add(this.btnTransactions);
            this.Controls.Add(this.btnDepositWithdraw);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnLoans);
            this.Controls.Add(this.btnLoanRequest);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnSupport);
            this.Name = "CustomerMain";
            this.Text = "Customer Main Menu";
            this.Load += new System.EventHandler(this.CustomerMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
