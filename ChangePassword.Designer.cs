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
            this.label2 = new System.Windows.Forms.Label();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.White;
            this.btnChange.FlatAppearance.BorderSize = 0;
            this.btnChange.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnChange.Location = new System.Drawing.Point(359, 659);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(205, 53);
            this.btnChange.TabIndex = 25;
            this.btnChange.Text = "Submit";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtOld
            // 
            this.txtOld.Location = new System.Drawing.Point(342, 169);
            this.txtOld.Name = "txtOld";
            this.txtOld.Size = new System.Drawing.Size(360, 22);
            this.txtOld.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Old Password";
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(342, 215);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(360, 22);
            this.txtNew.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "New Password";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(342, 255);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(360, 22);
            this.txtConfirm.TabIndex = 20;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(176, 258);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(145, 16);
            this.lblAmount.TabIndex = 19;
            this.lblAmount.Text = "Confirm New Password";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Location = new System.Drawing.Point(667, 661);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 47);
            this.button1.TabIndex = 26;
            this.button1.Text = "Go Back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 785);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtOld);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lblAmount);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangePassword";
            this.Text = "ChangePassword";
            this.Load += new System.EventHandler(this.ChangePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Button button1;
    }
}