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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Customer_Click(object sender, EventArgs e)
        {
            Customer cr = new Customer();
            cr.Show();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            Account acc = new Account();
            acc.Show();
            this.Close();
        }
    }
}
