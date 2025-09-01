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

        private void label7_Click(object sender, EventArgs e)
        {
            About 
        }
    }
}
