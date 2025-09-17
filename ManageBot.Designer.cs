namespace Bank__Management_System
{
    partial class ManageBot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageBot));
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.btnRefreshs = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefreshs)).BeginInit();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(1, -3);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1277, 788);
            this.webView21.TabIndex = 0;
            this.webView21.ZoomFactor = 1D;
            // 
            // btnRefreshs
            // 
            this.btnRefreshs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshs.BackgroundImage")));
            this.btnRefreshs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshs.Location = new System.Drawing.Point(1102, 30);
            this.btnRefreshs.Name = "btnRefreshs";
            this.btnRefreshs.Size = new System.Drawing.Size(23, 26);
            this.btnRefreshs.TabIndex = 4;
            this.btnRefreshs.TabStop = false;
            this.btnRefreshs.Click += new System.EventHandler(this.btnRefreshs_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(936, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // ManageBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 785);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefreshs);
            this.Controls.Add(this.webView21);
            this.Name = "ManageBot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManageBot";
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefreshs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.PictureBox btnRefreshs;
        private System.Windows.Forms.Label label1;
    }
}