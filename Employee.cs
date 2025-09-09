using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void LoadEmployee()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee", con);
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
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Employee (EID, Name, Position, Salary) VALUES (@EID, @Name, @Position, @Salary)", con);

                    cmd.Parameters.AddWithValue("@EID", int.Parse(txtEmpId.Text));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Employee record saved successfully");
                    btnClear_Click(null, null);
                    LoadEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving record: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Employee SET Name=@Name, Position=@Position, Salary=@Salary WHERE EID=@EID", con);

                    cmd.Parameters.AddWithValue("@EID", int.Parse(txtEmpId.Text));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        MessageBox.Show("Employee record updated successfully");
                    else
                        MessageBox.Show("No record found with that Employee ID.");

                    LoadEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating record: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE EID=@EID", con);
                    cmd.Parameters.AddWithValue("@EID", int.Parse(txtEmpId.Text));

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Employee record deleted successfully");
                        btnClear_Click(null, null);
                    }
                    else
                        MessageBox.Show("No record found with that Employee ID.");

                    LoadEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpId.Clear();
            txtName.Clear();
            txtPosition.Clear();
            txtSalary.Clear();
            txtEmpId.Focus();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Main admins = new Main();
            admins.Show();
            this.Hide();
        }
    }
}
