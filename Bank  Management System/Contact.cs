using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
        }

        private void call_Click(object sender, EventArgs e)
        {
            string url = "https://wa.me/94707266991";
            System.Diagnostics.Process.Start(url);
        }

        private void fb_Click(object sender, EventArgs e)
        {
            string url = "www.facebook.com";
            System.Diagnostics.Process.Start(url);
        }

        private void tiktok_Click(object sender, EventArgs e)
        {
            string url = "www.tiktok.com";
            System.Diagnostics.Process.Start(url);
        }

        private void instagram_Click(object sender, EventArgs e)
        {
            string url = "www.instagram.com";
            System.Diagnostics.Process.Start(url);
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            Landing landing = new Landing();
            landing.Show();
            this.Hide();
        }
    }
}
