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
            this.btnSubmitLoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitLoan.Location = new System.Drawing.Point(666, 386);
            this.btnSubmitLoan.Name = "btnSubmitLoan";
            this.btnSubmitLoan.Size = new System.Drawing.Size(228, 47);
            this.btnSubmitLoan.TabIndex = 10;
            this.btnSubmitLoan.Text = "Submit";
            this.btnSubmitLoan.UseVisualStyleBackColor = true;
            this.btnSubmitLoan.Click += new System.EventHandler(this.btnSubmitLoan_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(523, 219);
            this.txtAmount.Multiline = true;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(313, 36);
            this.txtAmount.TabIndex = 9;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Yi Baiti", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(365, 224);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(91, 28);
            this.lblAmount.TabIndex = 7;
            this.lblAmount.Text = "Amount";
            // 
            // lblLoanType
            // 
            this.lblLoanType.AutoSize = true;
            this.lblLoanType.Font = new System.Drawing.Font("Microsoft Yi Baiti", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoanType.Location = new System.Drawing.Point(365, 160);
            this.lblLoanType.Name = "lblLoanType";
            this.lblLoanType.Size = new System.Drawing.Size(128, 28);
            this.lblLoanType.TabIndex = 6;
            this.lblLoanType.Text = "Loan Type";
            // 
            // btnGoBack
            // 
            this.btnGoBack.BackColor = System.Drawing.Color.White;
            this.btnGoBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGoBack.FlatAppearance.BorderSize = 0;
            this.btnGoBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnGoBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBack.Location = new System.Drawing.Point(351, 386);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(238, 47);
            this.btnGoBack.TabIndex = 20;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.UseVisualStyleBackColor = false;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // cmbLoanType
            // 
            this.cmbLoanType.FormattingEnabled = true;
            this.cmbLoanType.Location = new System.Drawing.Point(523, 164);
            this.cmbLoanType.Name = "cmbLoanType";
            this.cmbLoanType.Size = new System.Drawing.Size(313, 24);
            this.cmbLoanType.TabIndex = 8;
            // 
            // dgvLoanRequests
            // 
            this.dgvLoanRequests.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoanRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoanRequests.GridColor = System.Drawing.Color.White;
            this.dgvLoanRequests.Location = new System.Drawing.Point(35, 477);
            this.dgvLoanRequests.Name = "dgvLoanRequests";
            this.dgvLoanRequests.RowHeadersWidth = 51;
            this.dgvLoanRequests.RowTemplate.Height = 24;
            this.dgvLoanRequests.Size = new System.Drawing.Size(1189, 266);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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