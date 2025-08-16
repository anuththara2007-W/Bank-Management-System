using System;

namespace BankApp
{
    partial class LoanRequest
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
            this.btnSubmitLoan = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblLoanType = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbLoanType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSubmitLoan
            // 
            this.btnSubmitLoan.Location = new System.Drawing.Point(397, 334);
            this.btnSubmitLoan.Name = "btnSubmitLoan";
            this.btnSubmitLoan.Size = new System.Drawing.Size(189, 30);
            this.btnSubmitLoan.TabIndex = 10;
            this.btnSubmitLoan.Text = "Submit";
            this.btnSubmitLoan.UseVisualStyleBackColor = true;
            this.btnSubmitLoan.Click += new System.EventHandler(this.btnSubmitLoan_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(351, 231);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(360, 22);
            this.txtAmount.TabIndex = 9;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(228, 234);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(52, 16);
            this.lblAmount.TabIndex = 7;
            this.lblAmount.Text = "Amount";
            // 
            // lblLoanType
            // 
            this.lblLoanType.AutoSize = true;
            this.lblLoanType.Location = new System.Drawing.Point(216, 157);
            this.lblLoanType.Name = "lblLoanType";
            this.lblLoanType.Size = new System.Drawing.Size(72, 16);
            this.lblLoanType.TabIndex = 6;
            this.lblLoanType.Text = "Loan Type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(387, 410);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 47);
            this.button1.TabIndex = 20;
            this.button1.Text = "Go Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbLoanType
            // 
            this.cmbLoanType.FormattingEnabled = true;
            this.cmbLoanType.Location = new System.Drawing.Point(361, 157);
            this.cmbLoanType.Name = "cmbLoanType";
            this.cmbLoanType.Size = new System.Drawing.Size(337, 24);
            this.cmbLoanType.TabIndex = 8;
            // 
            // LoanRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 522);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSubmitLoan);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.cmbLoanType);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblLoanType);
            this.Name = "LoanRequest";
            this.Text = "LoanRequest";
            this.Load += new System.EventHandler(this.LoanRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoanRequest_Load(object sender, EventArgs e)
        {
        }

        #endregion
        private System.Windows.Forms.Button btnSubmitLoan;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblLoanType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbLoanType;
    }
}