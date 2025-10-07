namespace BankApp
{
    partial class CustomerDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerDashboard));
            this.lblBalance = new System.Windows.Forms.Label();
            this.btnDeposit = new System.Windows.Forms.Button();
            this.btnWithdraw = new System.Windows.Forms.Button();
            this.btnLoanRequest = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.dgvLoans = new System.Windows.Forms.DataGridView();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblabout = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblHome = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(946, 145);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(32, 29);
            this.lblBalance.TabIndex = 3;
            this.lblBalance.Text = "0 ";
            // 
            // btnDeposit
            // 
            this.btnDeposit.BackColor = System.Drawing.Color.White;
            this.btnDeposit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeposit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDeposit.FlatAppearance.BorderSize = 0;
            this.btnDeposit.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnDeposit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDeposit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnDeposit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeposit.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnDeposit.Location = new System.Drawing.Point(50, 259);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(248, 44);
            this.btnDeposit.TabIndex = 4;
            this.btnDeposit.Text = "Deposit";
            this.btnDeposit.UseVisualStyleBackColor = false;
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.BackColor = System.Drawing.Color.White;
            this.btnWithdraw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWithdraw.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnWithdraw.FlatAppearance.BorderSize = 0;
            this.btnWithdraw.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnWithdraw.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnWithdraw.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnWithdraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWithdraw.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnWithdraw.Location = new System.Drawing.Point(345, 257);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(256, 44);
            this.btnWithdraw.TabIndex = 5;
            this.btnWithdraw.Text = "Withdraw";
            this.btnWithdraw.UseVisualStyleBackColor = false;
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // btnLoanRequest
            // 
            this.btnLoanRequest.BackColor = System.Drawing.Color.White;
            this.btnLoanRequest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoanRequest.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLoanRequest.FlatAppearance.BorderSize = 0;
            this.btnLoanRequest.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnLoanRequest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLoanRequest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnLoanRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoanRequest.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnLoanRequest.Location = new System.Drawing.Point(951, 259);
            this.btnLoanRequest.Name = "btnLoanRequest";
            this.btnLoanRequest.Size = new System.Drawing.Size(249, 44);
            this.btnLoanRequest.TabIndex = 6;
            this.btnLoanRequest.Text = "Loan Request";
            this.btnLoanRequest.UseVisualStyleBackColor = false;
            this.btnLoanRequest.Click += new System.EventHandler(this.btnLoanRequest_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.Color.White;
            this.btnTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransfer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTransfer.FlatAppearance.BorderSize = 0;
            this.btnTransfer.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnTransfer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnTransfer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnTransfer.Location = new System.Drawing.Point(653, 259);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(248, 44);
            this.btnTransfer.TabIndex = 7;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AllowUserToResizeColumns = false;
            this.dgvTransactions.AllowUserToResizeRows = false;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.GridColor = System.Drawing.Color.White;
            this.dgvTransactions.Location = new System.Drawing.Point(50, 428);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersWidth = 51;
            this.dgvTransactions.RowTemplate.Height = 24;
            this.dgvTransactions.Size = new System.Drawing.Size(574, 253);
            this.dgvTransactions.TabIndex = 8;
            // 
            // dgvLoans
            // 
            this.dgvLoans.AllowUserToAddRows = false;
            this.dgvLoans.AllowUserToResizeColumns = false;
            this.dgvLoans.AllowUserToResizeRows = false;
            this.dgvLoans.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoans.GridColor = System.Drawing.Color.White;
            this.dgvLoans.Location = new System.Drawing.Point(671, 428);
            this.dgvLoans.Name = "dgvLoans";
            this.dgvLoans.ReadOnly = true;
            this.dgvLoans.RowHeadersWidth = 51;
            this.dgvLoans.Size = new System.Drawing.Size(572, 253);
            this.dgvLoans.TabIndex = 9;
            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = System.Drawing.Color.White;
            this.btnProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProfile.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProfile.FlatAppearance.BorderSize = 0;
            this.btnProfile.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnProfile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnProfile.Location = new System.Drawing.Point(45, 341);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(246, 44);
            this.btnProfile.TabIndex = 10;
            this.btnProfile.Text = "Profile";
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.White;
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnChangePassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnChangePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnChangePassword.Location = new System.Drawing.Point(353, 338);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(246, 44);
            this.btnChangePassword.TabIndex = 11;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // btnSupport
            // 
            this.btnSupport.BackColor = System.Drawing.Color.White;
            this.btnSupport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupport.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSupport.FlatAppearance.BorderSize = 0;
            this.btnSupport.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnSupport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSupport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSupport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupport.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnSupport.Location = new System.Drawing.Point(655, 341);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(246, 44);
            this.btnSupport.TabIndex = 12;
            this.btnSupport.Text = "Support";
            this.btnSupport.UseVisualStyleBackColor = false;
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Uighur", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(511, 722);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(254, 43);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.Location = new System.Drawing.Point(215, 141);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(37, 32);
            this.lblCustomerName.TabIndex = 14;
            this.lblCustomerName.Text = "0 ";
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.White;
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.btnReport.Location = new System.Drawing.Point(951, 341);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(248, 44);
            this.btnReport.TabIndex = 15;
            this.btnReport.Text = "Reports";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblabout
            // 
            this.lblabout.AutoSize = true;
            this.lblabout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblabout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblabout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblabout.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblabout.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblabout.Location = new System.Drawing.Point(581, 53);
            this.lblabout.Name = "lblabout";
            this.lblabout.Size = new System.Drawing.Size(101, 22);
            this.lblabout.TabIndex = 32;
            this.lblabout.Text = "ABOUT  US ";
            this.lblabout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblabout.Click += new System.EventHandler(this.lblabout_Click);
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblContact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblContact.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblContact.Location = new System.Drawing.Point(763, 53);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(87, 22);
            this.lblContact.TabIndex = 31;
            this.lblContact.Text = "CONTACT";
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHome.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHome.Location = new System.Drawing.Point(428, 53);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(69, 22);
            this.lblHome.TabIndex = 30;
            this.lblHome.Text = "HOME  ";
            this.lblHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHome.Click += new System.EventHandler(this.lblHome_Click);
            // 
            // CustomerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 785);
            this.Controls.Add(this.lblabout);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblHome);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnSupport);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.dgvLoans);
            this.Controls.Add(this.dgvTransactions);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnLoanRequest);
            this.Controls.Add(this.btnWithdraw);
            this.Controls.Add(this.btnDeposit);
            this.Controls.Add(this.lblBalance);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomerDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerDashboard";
            this.Load += new System.EventHandler(this.CustomerDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Button btnDeposit;
        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnLoanRequest;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.DataGridView dgvLoans;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblabout;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblHome;
    }
}