namespace Bank__Management_System
{
    partial class Contact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contact));
            this.lblAbout = new System.Windows.Forms.Label();
            this.lblcontact1 = new System.Windows.Forms.Label();
            this.lblHome = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.instagram = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.instagram)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAbout.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbout.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblAbout.Location = new System.Drawing.Point(570, 41);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(87, 19);
            this.lblAbout.TabIndex = 15;
            this.lblAbout.Text = "ABOUT  US ";
            this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblcontact1
            // 
            this.lblcontact1.AutoSize = true;
            this.lblcontact1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblcontact1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblcontact1.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcontact1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcontact1.Location = new System.Drawing.Point(799, 40);
            this.lblcontact1.Name = "lblcontact1";
            this.lblcontact1.Size = new System.Drawing.Size(90, 22);
            this.lblcontact1.TabIndex = 14;
            this.lblcontact1.Text = "CONTACT";
            this.lblcontact1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHome.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHome.Location = new System.Drawing.Point(351, 41);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(60, 19);
            this.lblHome.TabIndex = 13;
            this.lblHome.Text = "HOME  ";
            this.lblHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(1120, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 30);
            this.label1.TabIndex = 16;
            this.label1.Text = "CONTACT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // instagram
            // 
            this.instagram.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("instagram.BackgroundImage")));
            this.instagram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.instagram.Location = new System.Drawing.Point(415, 486);
            this.instagram.Name = "instagram";
            this.instagram.Size = new System.Drawing.Size(57, 56);
            this.instagram.TabIndex = 17;
            this.instagram.TabStop = false;
            // 
            // Contact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 785);
            this.Controls.Add(this.instagram);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.lblcontact1);
            this.Controls.Add(this.lblHome);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Contact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contact";
            ((System.ComponentModel.ISupportInitialize)(this.instagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Label lblcontact1;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox instagram;
    }
}