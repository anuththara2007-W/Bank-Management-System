private void btnClear_Click(object sender, EventArgs e)
{
    ClearFields();
    dataGridView1.ClearSelection(); // Unselect any rows in the grid
}

private void ClearFields()
{
    txtTransactionID.Clear();
    txtTransactionType.Clear();
    txtAmount.Clear();
    txtAccountID.Clear();
    dateTimePicker1.Value = DateTime.Today;
    dateTimePicker1.CustomFormat = "dd/MM/yyyy";
    txtTransactionID.Focus();
}

private void btnDelete_Click(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtTransactionID.Text))
    {
        MessageBox.Show("Please enter a Transaction ID to delete.");
        return;
    }

    try
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Transactions WHERE TID = @TID", con);
            cmd.Parameters.AddWithValue("@TID", int.Parse(txtTransactionID.Text));

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                MessageBox.Show("Record deleted successfully");
                LoadTransactions();
                ClearFields(); // Clear inputs after deleting
            }
            else
            {
                MessageBox.Show("No record found with the specified Transaction ID.");
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error deleting record: " + ex.Message);
    }
}

private void btnUpdate_Click(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtTransactionID.Text) ||
        string.IsNullOrWhiteSpace(txtTransactionType.Text) ||
        string.IsNullOrWhiteSpace(txtAmount.Text) ||
        string.IsNullOrWhiteSpace(txtAccountID.Text))
    {
        MessageBox.Show("Please fill in all fields before updating.");
        return;
    }

    try
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(
                "UPDATE Transactions SET Transaction_Type = @Transaction_Type, Amount = @Amount, " +
                "Transaction_Date = @Transaction_Date, Account_ID = @Account_ID WHERE TID = @TID", con);

            cmd.Parameters.AddWithValue("@TID", int.Parse(txtTransactionID.Text));
            cmd.Parameters.AddWithValue("@Transaction_Type", txtTransactionType.Text);
            cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
            cmd.Parameters.AddWithValue("@Transaction_Date", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@Account_ID", int.Parse(txtAccountID.Text));

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                MessageBox.Show("Record updated successfully");
                LoadTransactions();
                ClearFields();
            }
            else
            {
                MessageBox.Show("No record found with the specified Transaction ID.");
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error updating record: " + ex.Message);
    }
}
