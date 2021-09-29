using Microsoft.Data.SqlClient;

namespace WebApiyamaha.Services.SQL
{
    public class DBConnection
    {
        private readonly string sqlParams = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Proger1\source\repos\Parser-for-AutoAll\Parts.mdf;User ID=UserAdmin;Password=Lorgar17";

        public SqlConnection sqlConnection;

        public DBConnection()
        {
            sqlConnection = new SqlConnection(sqlParams);
            sqlConnection.Open();
        }
        ~DBConnection()
        {
            sqlConnection.Close();
            /*if (sqlConnection.State == ConnectionState.Closed)
                Console.WriteLine("Connection closed");*/
        }
    }
}

