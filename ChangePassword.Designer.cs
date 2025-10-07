using System;

namespace BankApp
{
    partial class ChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePassword));
            this.btnChange = new System.Windows.Forms.Button();
            this.txtOld = new System.Windows.Forms.TextBox();
            this.lblOld = new System.Windows.Forms.Label();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.lblneww = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblconfirm = new System.Windows.Forms.Label();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.lblabout = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblHome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.White;
            this.btnChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChange.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnChange.FlatAppearance.BorderSize = 0;
            this.btnChange.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChange.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnChange.Location = new System.Drawing.Point(673, 660);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(205, 53);
            this.btnChange.TabIndex = 25;
            this.btnChange.Text = "Submit";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtOld
            // 
            this.txtOld.Location = new System.Drawing.Point(493, 282);
            this.txtOld.Multiline = true;
            this.txtOld.Name = "txtOld";
            this.txtOld.Size = new System.Drawing.Size(411, 34);
            this.txtOld.TabIndex = 24;
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.lblOld.Location = new System.Drawing.Point(249, 285);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(140, 26);
            this.lblOld.TabIndex = 23;
            this.lblOld.Text = "Old Password";
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(493, 344);
            this.txtNew.Multiline = true;
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(411, 34);
            this.txtNew.TabIndex = 22;
            // 
            // lblneww
            // 
            this.lblneww.AutoSize = true;
            this.lblneww.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.lblneww.Location = new System.Drawing.Point(249, 344);
            this.lblneww.Name = "lblneww";
            this.lblneww.Size = new System.Drawing.Size(150, 26);
            this.lblneww.TabIndex = 21;
            this.lblneww.Text = "New Password";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(493, 407);
            this.txtConfirm.Multiline = true;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(411, 34);
            this.txtConfirm.TabIndex = 20;
            // 
            // lblconfirm
            // 
            this.lblconfirm.AutoSize = true;
            this.lblconfirm.Font = new System.Drawing.Font("Yu Gothic", 12F);
            this.lblconfirm.Location = new System.Drawing.Point(249, 407);
            this.lblconfirm.Name = "lblconfirm";
            this.lblconfirm.Size = new System.Drawing.Size(229, 26);
            this.lblconfirm.TabIndex = 19;
            this.lblconfirm.Text = "Confirm New Password";
            // 
            // btnGoBack
            // 
            this.btnGoBack.BackColor = System.Drawing.Color.White;
            this.btnGoBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGoBack.FlatAppearance.BorderSize = 0;
            this.btnGoBack.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnGoBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnGoBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBack.Location = new System.Drawing.Point(352, 663);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(224, 47);
            this.btnGoBack.TabIndex = 26;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.UseVisualStyleBackColor = false;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // lblabout
            // 
            this.lblabout.AutoSize = true;
            this.lblabout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblabout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblabout.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblabout.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblabout.Location = new System.Drawing.Point(548, 52);
            this.lblabout.Name = "lblabout";
            this.lblabout.Size = new System.Drawing.Size(101, 22);
            this.lblabout.TabIndex = 29;
            this.lblabout.Text = "ABOUT  US ";
            this.lblabout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblabout.Click += new System.EventHandler(this.lblabout_Click);
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblContact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblContact.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblContact.Location = new System.Drawing.Point(730, 52);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(87, 22);
            this.lblContact.TabIndex = 28;
            this.lblContact.Text = "CONTACT";
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblContact.Click += new System.EventHandler(this.lblContact_Click);
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHome.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHome.Location = new System.Drawing.Point(395, 52);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(69, 22);
            this.lblHome.TabIndex = 27;
            this.lblHome.Text = "HOME  ";
            this.lblHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHome.Click += new System.EventHandler(this.label5_Click);
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 785);
            this.Controls.Add(this.lblabout);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblHome);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtOld);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.lblneww);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lblconfirm);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangePassword";
            this.Load += new System.EventHandler(this.ChangePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtOld;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.Label lblneww;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label lblconfirm;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.Label lblabout;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblHome;
    }
}