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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            Landing landing = new Landing();
            landing.Show();
            this.Hide();
        }

        private void lblcontact1_Click(object sender, EventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Hide();
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void lblcontact2_Click(object sender, EventArgs e)
        {
            string url = "https://wa.me/94707266991";
            System.Diagnostics.Process.Start(url);
        }
    }
}
