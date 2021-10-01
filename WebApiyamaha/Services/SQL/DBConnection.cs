using Microsoft.Data.SqlClient;
using System;

namespace WebApiyamaha.Services.SQL
{
    public class DBConnection : IDisposable
    {
        private readonly string sqlParams = @"Data Source=DESKTOP-N44A3NQ;Initial Catalog=C:\USERS\PROGER\SOURCE\REPOS\YAMAHAPARSER\PARSER\YAMAHA.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public SqlConnection sqlConnection;

        public DBConnection()
        {
            sqlConnection = new SqlConnection(sqlParams);
            sqlConnection.Open();
        }
        public void Dispose()
        {
            sqlConnection.Close();
        }
        ~DBConnection()
        {
            sqlConnection.Close();
        }
    }
}

