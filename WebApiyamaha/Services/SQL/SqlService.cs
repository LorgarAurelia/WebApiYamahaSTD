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
        private static List<ModelsInfo> GetModelsInfo(string id = "0", string table = "empty")
        {
            using DBConnection sqlClient = new();

            List<ModelsInfo> content = new();

            string query = $"SELECT Displacement FROM Displacement where productId = {id}";


            switch (table)
            {
                case "empty":
                    query = "select ProductName, productId from Categories"; 
                    break;
                case "Displacement":
                case "displacement":
                    query = $"SELECT Displacement, id FROM Displacement where productId = {id}";
                    break;
                case "Model":
                    query = $"select dispModelName, id from Models where displacementID = {id}";
                    break;
                case "Year":
                    query = $"select modelYears, id from ModelYears where modelId = {id}";
                    break;
            }

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ModelsInfo row = new();
                row.Id = Convert.ToInt32(reader[1].ToString().Trim());
                row.Value = reader[0].ToString().Trim();

                content.Add(row);
            }
            reader.Close();

            return content;
        }

        
        public static List<ModelsInfo> GetFromDataBase(string table, string keyForSearch = "empty")
        {
            List<ModelsInfo> content = new();
            switch (table)
            {
                case "Categories":
                    content = GetModelsInfo();
                    break;
                case "Diseplacement":
                    content = GetModelsInfo(keyForSearch, table);
                    break;
                
            }
            return content;
            
        }
    }
}
