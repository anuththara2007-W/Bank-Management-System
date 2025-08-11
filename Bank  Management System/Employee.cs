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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Loan", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Employee VALUES (@LoanID,@LoanType,@Amount,@InterestRate,@LoanDate,@CustomerName)", con);

                    cmd.Parameters.AddWithValue("@LoanID", int.Parse(txtEmpId.Text));
                    cmd.Parameters.AddWithValue("@LoanType", txtName.Text);
                    cmd.Parameters.AddWithValue("@InterestRate", decimal.Parse(txtPosition.Text));
                    cmd.Parameters.AddWithValue("@CustomerName", txtSalary.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Loan record saved successfully");
                    btnClear_Click(null, null);
                    LoadEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving record: " + ex.Message);
                }
            }
        }
    }
}
