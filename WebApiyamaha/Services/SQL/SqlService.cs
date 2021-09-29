using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiyamaha.Models;

namespace WebApiyamaha.Services.SQL
{
    public class SqlService : DBConnection
    {
        private static List<string> GetDisplacement(string product_Id)
        {
            DBConnection sqlClient = new();

            List<string> displacements = new();

            string query = $"SELECT Displacement FROM Displacement where productId = {product_Id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string row;

                row = reader[0].ToString().Trim();

                displacements.Add(row);
            }
            reader.Close();

            sqlClient = null;

            return displacements;
        }

        private static List<Categories> GetSimpleData()
        {
            List<Categories> content = new();
            return content;
        }
        public static List<string> GetFromDataBase(string keyForSearch, string table)
        {
            List<string> content = new();

            switch (table)
            {
                case "Categories":
                    content = GetSimpleData();
                    break;
                case "diseplacement":
                    content = GetDisplacement(keyForSearch);
                    break;
                default:
                    break;
            }

            return content;
        }
    }
}
