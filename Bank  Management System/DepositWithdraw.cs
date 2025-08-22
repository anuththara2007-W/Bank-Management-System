private void LoadAccountsToGrid()
{
    if (GetCustomerID() == 0) return;

    try
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT Account_ID, Account_Type, Balance FROM Accounts WHERE Customer_ID=@cid",
                con);
            cmd.Parameters.AddWithValue("@cid", GetCustomerID());

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            gridAccounts.DataSource = dt;
            gridAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 🔹 Auto-select the first row
            if (gridAccounts.Rows.Count > 0)
            {
                gridAccounts.Rows[0].Selected = true;
                selectedAccId = Convert.ToInt32(gridAccounts.Rows[0].Cells["Account_ID"].Value);
                currentBal = Convert.ToDecimal(gridAccounts.Rows[0].Cells["Balance"].Value);
                lblBalance.Text = $"Account: {selectedAccId} ({gridAccounts.Rows[0].Cells["Account_Type"].Value})\nBalance: ${currentBal:F2}";
            }

            gridAccounts.Refresh(); // Force UI to redraw
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error loading accounts: " + ex.Message);
    }
}

// ✅ After deposit or withdraw, call this to refresh immediately
private void RefreshGrid()
{
    LoadAccountsToGrid(); // reload from DB
    gridAccounts.Refresh(); // redraw UI
}
