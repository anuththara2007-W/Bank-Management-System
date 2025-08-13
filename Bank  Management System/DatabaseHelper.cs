using System.Data.SqlClient;

namespace BankApp
{
    public static class DatabaseHelper
    {
        public static string ConnectionString =
            @"Data Source=(localdb)\Local;Initial Catalog=BankDB;Integrated Security=True;Encrypt=False";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
