using System;
using System.Data.SqlClient;

namespace WebApiyamaha.Services.SQL
{
    public class DBConnection : IDisposable
    {
        public SqlConnection sqlConnection;

        public DBConnection(string connectionString)
        {

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }
        public void Dispose()
        {
            sqlConnection.Close();
        }
    }
}

