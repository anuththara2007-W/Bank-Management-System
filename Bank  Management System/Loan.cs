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
    public partial class Loan : Form
    {
        public Loan()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                dateTimePicker1.CustomFormat = " ";
            }
        }

    }
}
