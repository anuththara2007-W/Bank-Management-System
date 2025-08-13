using System;

namespace BankApp
{
    partial class TransferFunds
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
            this.btnWithdraw = new System.Windows.Forms.Button();
            this.btnDeposit = new System.Windows.Forms.Button();
            this.txtToAccountID = new System.Windows.Forms.TextBox();
            this.cmbFromAccount = new System.Windows.Forms.ComboBox();
            this.lblToAccount = new System.Windows.Forms.Label();
            this.lblFromAccount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.Location = new System.Drawing.Point(460, 299);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(189, 30);
            this.btnWithdraw.TabIndex = 11;
            this.btnWithdraw.Text = "Withdraw";
            this.btnWithdraw.UseVisualStyleBackColor = true;
            // 
            // btnDeposit
            // 
            this.btnDeposit.Location = new System.Drawing.Point(152, 299);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(189, 30);
            this.btnDeposit.TabIndex = 10;
            this.btnDeposit.Text = "Deposit ";
            this.btnDeposit.UseVisualStyleBackColor = true;
            // 
            // txtToAccountID
            // 
            this.txtToAccountID.Location = new System.Drawing.Point(289, 195);
            this.txtToAccountID.Name = "txtToAccountID";
            this.txtToAccountID.Size = new System.Drawing.Size(360, 22);
            this.txtToAccountID.TabIndex = 9;
            // 
            // cmbFromAccount
            // 
            this.cmbFromAccount.FormattingEnabled = true;
            this.cmbFromAccount.Location = new System.Drawing.Point(299, 121);
            this.cmbFromAccount.Name = "cmbFromAccount";
            this.cmbFromAccount.Size = new System.Drawing.Size(337, 24);
            this.cmbFromAccount.TabIndex = 8;
            // 
            // lblToAccount
            // 
            this.lblToAccount.AutoSize = true;
            this.lblToAccount.Location = new System.Drawing.Point(166, 198);
            this.lblToAccount.Name = "lblToAccount";
            this.lblToAccount.Size = new System.Drawing.Size(52, 16);
            this.lblToAccount.TabIndex = 7;
            this.lblToAccount.Text = "Amount";
            // 
            // lblFromAccount
            // 
            this.lblFromAccount.AutoSize = true;
            this.lblFromAccount.Location = new System.Drawing.Point(154, 121);
            this.lblFromAccount.Name = "lblFromAccount";
            this.lblFromAccount.Size = new System.Drawing.Size(96, 16);
            this.lblFromAccount.TabIndex = 6;
            this.lblFromAccount.Text = "Select Account";
            // 
            // TransferFunds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnWithdraw);
            this.Controls.Add(this.btnDeposit);
            this.Controls.Add(this.txtToAccountID);
            this.Controls.Add(this.cmbFromAccount);
            this.Controls.Add(this.lblToAccount);
            this.Controls.Add(this.lblFromAccount);
            this.Name = "TransferFunds";
            this.Text = "TransferFunds";
            this.Load += new System.EventHandler(this.TransferFunds_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TransferFunds_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnDeposit;
        private System.Windows.Forms.TextBox txtToAccountID;
        private System.Windows.Forms.ComboBox cmbFromAccount;
        private System.Windows.Forms.Label lblToAccount;
        private System.Windows.Forms.Label lblFromAccount;
    }
}