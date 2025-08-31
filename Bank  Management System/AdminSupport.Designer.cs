namespace Bank__Management_System
{
    partial class AdminSupport
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
            this.dgvSupport = new System.Windows.Forms.DataGridView();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.btnReply = new System.Windows.Forms.Button();
            this.txtReply = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupport)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSupport
            // 
            this.dgvSupport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupport.Location = new System.Drawing.Point(181, 218);
            this.dgvSupport.Name = "dgvSupport";
            this.dgvSupport.RowHeadersWidth = 51;
            this.dgvSupport.RowTemplate.Height = 24;
            this.dgvSupport.Size = new System.Drawing.Size(690, 150);
            this.dgvSupport.TabIndex = 35;
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(472, 397);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(141, 47);
            this.btnGoBack.TabIndex = 34;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.UseVisualStyleBackColor = true;
            // 
            // btnReply
            // 
            this.btnReply.Location = new System.Drawing.Point(445, 158);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(189, 30);
            this.btnReply.TabIndex = 33;
            this.btnReply.Text = "Send";
            this.btnReply.UseVisualStyleBackColor = true;
            this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
            // 
            // txtReply
            // 
            this.txtReply.Location = new System.Drawing.Point(435, 81);
            this.txtReply.Name = "txtReply";
            this.txtReply.Size = new System.Drawing.Size(360, 22);
            this.txtReply.TabIndex = 32;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(269, 84);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(64, 16);
            this.lblAmount.TabIndex = 31;
            this.lblAmount.Text = "Message";
            // 
            // AdminSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 525);
            this.Controls.Add(this.dgvSupport);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnReply);
            this.Controls.Add(this.txtReply);
            this.Controls.Add(this.lblAmount);
            this.Name = "AdminSupport";
            this.Text = "AdminSupport";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSupport;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.Button btnReply;
        private System.Windows.Forms.TextBox txtReply;
        private System.Windows.Forms.Label lblAmount;
    }
}