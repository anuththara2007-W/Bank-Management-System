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
            this.button1 = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 47);
            this.button1.TabIndex = 34;
            this.button1.Text = "Go Back";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(445, 158);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(189, 30);
            this.btnSend.TabIndex = 33;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(435, 81);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(360, 22);
            this.txtMessage.TabIndex = 32;
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblAmount);
            this.Name = "AdminSupport";
            this.Text = "AdminSupport";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSupport;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblAmount;
    }
}