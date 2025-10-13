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
    public partial class Landing : Form
    {
        public Landing()
        {
            InitializeComponent();
        }

        private void btnsignin_Click(object sender, EventArgs e)
        {
            Login gonow = new Login();
            gonow.Show();
            this.Hide();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About About = new About();
            About.Show();
            this.Hide();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            Contact contact =  new Contact();
            contact.Show();
            this.Hide();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Contact the bank ! ");
            string url = "https://wa.me/94707266991";
            System.Diagnostics.Process.Start(url);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            ChatBot bot = new ChatBot();
            bot.Show();
            this.Hide();
        }
    }
}
