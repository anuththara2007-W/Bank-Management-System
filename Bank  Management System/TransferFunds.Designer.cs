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
            this.btnTransfer = new System.Windows.Forms.Button();
            this.txtToAccountID = new System.Windows.Forms.TextBox();
            this.lblToAccount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtAmounts = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(299, 296);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(189, 30);
            this.btnTransfer.TabIndex = 10;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            // 
            // txtToAccountID
            // 
            this.txtToAccountID.Location = new System.Drawing.Point(289, 177);
            this.txtToAccountID.Name = "txtToAccountID";
            this.txtToAccountID.Size = new System.Drawing.Size(360, 22);
            this.txtToAccountID.TabIndex = 9;
            // 
            // lblToAccount
            // 
            this.lblToAccount.AutoSize = true;
            this.lblToAccount.Location = new System.Drawing.Point(166, 180);
            this.lblToAccount.Name = "lblToAccount";
            this.lblToAccount.Size = new System.Drawing.Size(52, 16);
            this.lblToAccount.TabIndex = 7;
            this.lblToAccount.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(289, 223);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(360, 22);
            this.txtAmount.TabIndex = 13;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(166, 226);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(52, 16);
            this.lblAmount.TabIndex = 12;
            this.lblAmount.Text = "Amount";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(327, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 47);
            this.button1.TabIndex = 22;
            this.button1.Text = "Go Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtAmounts
            // 
            this.txtAmounts.Location = new System.Drawing.Point(289, 133);
            this.txtAmounts.Name = "txtAmounts";
            this.txtAmounts.Size = new System.Drawing.Size(360, 22);
            this.txtAmounts.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Amount";
            // 
            // TransferFunds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtAmounts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.txtToAccountID);
            this.Controls.Add(this.lblToAccount);
            this.Name = "TransferFunds";
            this.Text = "TransferFunds";
            this.Load += new System.EventHandler(this.TransferFunds_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TransferFunds_Load(object sender, EventArgs e)
        {
        }

        #endregion
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.TextBox txtToAccountID;
        private System.Windows.Forms.Label lblToAccount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtAmounts;
        private System.Windows.Forms.Label label1;
    }
}