using System;

namespace BankApp
{
    partial class AdminLoanRequests
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
            this.dgvAllRequests = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAllRequests
            // 
            this.dgvAllRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllRequests.Location = new System.Drawing.Point(23, 267);
            this.dgvAllRequests.Name = "dgvAllRequests";
            this.dgvAllRequests.RowHeadersWidth = 51;
            this.dgvAllRequests.RowTemplate.Height = 24;
            this.dgvAllRequests.Size = new System.Drawing.Size(1109, 183);
            this.dgvAllRequests.TabIndex = 0;
            // 
            // AdminLoanRequests
            // 
            this.ClientSize = new System.Drawing.Size(1170, 570);
            this.Controls.Add(this.dgvAllRequests);
            this.Name = "AdminLoanRequests";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllRequests)).EndInit();
            this.ResumeLayout(false);

        }

        private void LoanRequestsApproval_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.DataGridView dgvAllRequests;
    }
}