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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanRequest));
            this.btnSubmitLoan = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblLoanType = new System.Windows.Forms.Label();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.cmbLoanType = new System.Windows.Forms.ComboBox();
            this.dgvLoanRequests = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmitLoan
            // 
            this.btnSubmitLoan.Location = new System.Drawing.Point(386, 213);
            this.btnSubmitLoan.Name = "btnSubmitLoan";
            this.btnSubmitLoan.Size = new System.Drawing.Size(189, 30);
            this.btnSubmitLoan.TabIndex = 10;
            this.btnSubmitLoan.Text = "Submit";
            this.btnSubmitLoan.UseVisualStyleBackColor = true;
            this.btnSubmitLoan.Click += new System.EventHandler(this.btnSubmitLoan_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(551, 249);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(360, 22);
            this.txtAmount.TabIndex = 9;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(237, 161);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(52, 16);
            this.lblAmount.TabIndex = 7;
            this.lblAmount.Text = "Amount";
            // 
            // lblLoanType
            // 
            this.lblLoanType.AutoSize = true;
            this.lblLoanType.Location = new System.Drawing.Point(205, 36);
            this.lblLoanType.Name = "lblLoanType";
            this.lblLoanType.Size = new System.Drawing.Size(72, 16);
            this.lblLoanType.TabIndex = 6;
            this.lblLoanType.Text = "Loan Type";
            this.lblLoanType.Click += new System.EventHandler(this.lblLoanType_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(376, 289);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(141, 47);
            this.btnGoBack.TabIndex = 20;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // cmbLoanType
            // 
            this.cmbLoanType.FormattingEnabled = true;
            this.cmbLoanType.Location = new System.Drawing.Point(623, 170);
            this.cmbLoanType.Name = "cmbLoanType";
            this.cmbLoanType.Size = new System.Drawing.Size(337, 24);
            this.cmbLoanType.TabIndex = 8;
            // 
            // dgvLoanRequests
            // 
            this.dgvLoanRequests.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoanRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoanRequests.GridColor = System.Drawing.Color.White;
            this.dgvLoanRequests.Location = new System.Drawing.Point(3, 362);
            this.dgvLoanRequests.Name = "dgvLoanRequests";
            this.dgvLoanRequests.RowHeadersWidth = 51;
            this.dgvLoanRequests.RowTemplate.Height = 24;
            this.dgvLoanRequests.Size = new System.Drawing.Size(908, 127);
            this.dgvLoanRequests.TabIndex = 21;
            // 
            // LoanRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 785);
            this.Controls.Add(this.dgvLoanRequests);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnSubmitLoan);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.cmbLoanType);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblLoanType);
            this.DoubleBuffered = true;
            this.Name = "LoanRequest";
            this.Text = "LoanRequest";
            this.Load += new System.EventHandler(this.LoanRequest_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanRequests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion
        private System.Windows.Forms.Button btnSubmitLoan;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblLoanType;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.ComboBox cmbLoanType;
        private System.Windows.Forms.DataGridView dgvLoanRequests;
    }
}