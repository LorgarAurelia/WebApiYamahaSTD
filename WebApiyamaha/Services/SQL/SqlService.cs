using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApiyamaha.Services.SQL.Models;

namespace WebApiyamaha.Services.SQL
{
    public class SqlService
    {
        private static async Task<List<SqlDataModel>> ModelDataReader(string query, DBConnection sqlClient)
        {
            List<SqlDataModel> content = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SqlDataModel row = new();
                row.Id = reader[1].ToString();
                row.Value = reader[0].ToString();

                content.Add(row);
            }
            reader.Close();

            return content;
        }
        /// <summary>
        /// Метод получает таблицу категорий.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static async Task<List<SqlDataModel>> GetCategoriesAsync(string sqlConnection)
        {
            using DBConnection sqlClient = new(sqlConnection);

            List<SqlDataModel> content = new();

            string query = "select ProductName, productId from Categories";

            content = await ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Метод получает данные по объёму двигателей в выбранной категории.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<SqlDataModel>> GetDiseplacement(string sqlConnection, string productId)
        {
            using DBConnection sqlClient = new(sqlConnection);

            List<SqlDataModel> content = new();

            string query = $"SELECT Displacement, id FROM Displacement where productId = {productId}";

            content = await ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Метод получает данные о всех моделях с выбранным объёмом двигателя.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<SqlDataModel>> GetModelAsync(string sqlConnection, string id)
        {
            using DBConnection sqlClient = new(sqlConnection);

            List<SqlDataModel> content = new();

            string query = $"select dispModelName, id from Models where displacementID = {id}";

            content = await ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Метод получает данные о всех годах выпуска у выбранных моделей.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<SqlDataModel>> GetYearsAsync(string sqlConnection, string id)
        {
            using DBConnection sqlClient = new(sqlConnection);

            List<SqlDataModel> content = new();

            string query = $"select modelYears, id from ModelYears where modelId = {id}";

            content = await ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Мотвед отвечающий за таблицу моделей по цветам.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<SqlDataModel>> GetModelsList(string sqlConnection, string id)
        {
            using DBConnection sqlClient = new(sqlConnection);

            List<SqlDataModel> content = new();

            string query = $"select distinct modelTypeCode, colorName, pic.Name, v.Id from Variants as v left join  Pictures as pic on v.Id = pic.VariantsId where YearsId = {id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SqlDataModel row = new();
                string name = reader[0].ToString() + " ";
                name += reader[1].ToString();
                row.Value = name;
                row.Image = reader[2].ToString();
                row.Id = reader[3].ToString();

                content.Add(row);
            }

            reader.Close();

            return content;
        }

        /// <summary>
        /// Метод получает Каталог в Формате JSON по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<SqlDataModel>> GetCatalog(string sqlConnection, string id)
        {
            using DBConnection sqlClient = new(sqlConnection);

            List<SqlDataModel> content = new();

            string query = $"select cat.Id, cat.figName, cat.figNo from Cataloge as cat left join PartsPicture as pic on pic.catalogeId = cat.Id where cat.VariantId = {id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SqlDataModel row = new();
                string name;
                row.Id = reader[0].ToString();
                row.Value = $@"{reader[1].ToString()} № {reader[2].ToString()}";

                content.Add(row);
            }
            reader.Close();

            return content;
        }

        /// <summary>
        /// Получение информации касательно конкретной запчасти в формате JSON по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<PartComponentsSql>> GetPart(string connectionString, string id)
        {
            using DBConnection sqlClient = new(connectionString);

            List<PartComponentsSql> content = new();

            string query = $"select distinct p.partNo, p.quantity, p.remarks, p.partName, pic.name from Parts as p left join PartsPicture as pic on p.CatalogeId = pic.catalogeId where p.CatalogeId = {id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PartComponentsSql row = new();

                row.PartNo = reader[0].ToString();
                row.Quantity = reader[1].ToString();
                row.Remarks = reader[2].ToString();
                row.PartName = reader[3].ToString();
                row.ImageName = reader[4].ToString();

                content.Add(row);
            }
            reader.Close();

            return content;
        }
    }
}
