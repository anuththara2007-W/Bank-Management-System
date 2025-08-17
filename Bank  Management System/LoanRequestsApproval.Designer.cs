using System;

namespace BankApp
{
    partial class LoanRequestsApproval
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
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(188, 96);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(180, 44);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(629, 89);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(232, 58);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(460, 497);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 49);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // LoanRequestsApproval
            // 
            this.ClientSize = new System.Drawing.Size(1170, 570);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.dgvAllRequests);
            this.Name = "LoanRequestsApproval";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllRequests)).EndInit();
            this.ResumeLayout(false);

        }

        private void LoanRequestsApproval_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAllRequests;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button button1;
    }
}