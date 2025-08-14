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
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(20, 20);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(100, 16);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Welcome, User";
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(20, 60);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(75, 23);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnAccounts
            // 
            this.btnAccounts.Location = new System.Drawing.Point(20, 100);
            this.btnAccounts.Name = "btnAccounts";
            this.btnAccounts.Size = new System.Drawing.Size(75, 23);
            this.btnAccounts.TabIndex = 2;
            this.btnAccounts.Text = "My Accounts";
            this.btnAccounts.Click += new System.EventHandler(this.btnAccounts_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.Location = new System.Drawing.Point(235, 136);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(75, 23);
            this.btnTransactions.TabIndex = 3;
            // 
            // btnDepositWithdraw
            // 
            this.btnDepositWithdraw.Location = new System.Drawing.Point(363, 115);
            this.btnDepositWithdraw.Name = "btnDepositWithdraw";
            this.btnDepositWithdraw.Size = new System.Drawing.Size(75, 23);
            this.btnDepositWithdraw.TabIndex = 4;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(300, 20);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnTransfer.TabIndex = 5;
            // 
            // btnLoans
            // 
            this.btnLoans.Location = new System.Drawing.Point(0, 0);
            this.btnLoans.Name = "btnLoans";
            this.btnLoans.Size = new System.Drawing.Size(75, 23);
            this.btnLoans.TabIndex = 6;
            // 
            // btnLoanRequest
            // 
            this.btnLoanRequest.Location = new System.Drawing.Point(0, 0);
            this.btnLoanRequest.Name = "btnLoanRequest";
            this.btnLoanRequest.Size = new System.Drawing.Size(75, 23);
            this.btnLoanRequest.TabIndex = 7;
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(0, 0);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(75, 23);
            this.btnProfile.TabIndex = 8;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(0, 0);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(75, 23);
            this.btnChangePassword.TabIndex = 9;
            // 
            // btnSupport
            // 
            this.btnSupport.Location = new System.Drawing.Point(0, 0);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(75, 23);
            this.btnSupport.TabIndex = 10;
            // 
            // CustomerMain
            // 
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
