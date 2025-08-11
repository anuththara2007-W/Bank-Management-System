using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            display1();
            display2();
            display3();
        }


        private void display1()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM emptab", con);
                Int32 count = Convert.ToInt32(comm.ExecuteScalar());
                if (count > 0)
                {
                    lblCount1.Text = count.ToString(count.ToString());


                }
                else
                {
                    lblCount1.Text = "0";
                }
                con.Close();
            }
        }

        private void display2()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM emptab", con);
                Int32 count = Convert.ToInt32(comm.ExecuteScalar());
                if (count > 0)
                {
                    lblCount2.Text = count.ToString(count.ToString());


                }
                else
                {
                    lblCount2.Text = "0";
                }
                con.Close();
            }
        }

        private void display3()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM emptab", con);
                Int32 count = Convert.ToInt32(comm.ExecuteScalar());
                if (count > 0)
                {
                    lblCount3.Text = count.ToString(count.ToString());


                }
                else
                {
                    lblCount3.Text = "0";
                }
                con.Close();
            }
        }
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Login gonow = new Login();
            gonow.Show();
            this.Hide();
        }
    }
}
