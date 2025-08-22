using BankApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

private void LoadAccountsToGrid()
{
    try
    {
        int customerIdToUse = GetCustomerID();
        if (customerIdToUse == 0)
        {
            MessageBox.Show("Customer ID not found. Please login again.");
            return;
        }

        using (SqlConnection con = DatabaseHelper.GetConnection())
        {
            con.Open();
            string query = "SELECT Account_ID, Account_Type, Balance FROM accounts WHERE Customer_ID = @cid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@cid", customerIdToUse);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gridAccounts.DataSource = dt; // gridAccounts = your DataGridView name
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error loading grid: " + ex.Message);
    }
}
