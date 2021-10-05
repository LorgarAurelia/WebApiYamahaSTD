using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiyamaha.Models;

namespace WebApiyamaha.Services.SQL
{
    public class SqlService : DBConnection
    {
        private static List<ModelsInfo> ModelDataReader(string query, DBConnection sqlClient)
        {
            List<ModelsInfo> content = new();

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ModelsInfo row = new();
                row.Id = Convert.ToInt32(reader[1].ToString().Trim());
                row.Value = reader[0].ToString().Trim().Replace("+", "'");

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
        public static List<ModelsInfo> GetCategories()
        {
            using DBConnection sqlClient = new();

            List<ModelsInfo> content = new();

            string query = "select ProductName, productId from Categories";

            content = ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Метод получает данные по объёму двигателей в выбранной категории.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ModelsInfo> GetDiseplacement(string id)
        {
            using DBConnection sqlClient = new();

            List<ModelsInfo> content = new();

            string query = $"SELECT Displacement, id FROM Displacement where productId = {id}"; 

            content = ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Метод получает данные о всех моделях с выбранным объёмом двигателя.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ModelsInfo> GetModel(string id)
        {
            using DBConnection sqlClient = new();

            List<ModelsInfo> content = new();

            string query = $"select dispModelName, id from Models where displacementID = {id}";

            content = ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Метод получает данные о всех годах выпуска у выбранных моделей.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ModelsInfo> GetYears(string id)
        {
            using DBConnection sqlClient = new();

            List<ModelsInfo> content = new();

            string query = $"select modelYears, id from ModelYears where modelId = {id}";

            content = ModelDataReader(query, sqlClient);

            return content;
        }

        /// <summary>
        /// Мотвед отвечающий за таблицу моделей по цветам.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ModelsList> GetModelsList(string id)
        {
            using DBConnection sqlClient = new();

            List<ModelsList> content = new();

            string query = $"SELECT v.Id, my.modelYears, v.modelTypeCode, v.productNo, v.colorType, v.colorName, mk.modelName FROM Variants AS v JOIN ModelYears AS my ON v.YearsId = my.Id JOIN Models AS mk ON my.modelId = mk.Id WHERE v.YearsId = {id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ModelsList row = new();
                row.Id = Convert.ToInt32(reader[0].ToString().Trim());
                row.ModelYears = Convert.ToInt32(reader[1].ToString().Trim());
                row.ModelTypeCode = reader[2].ToString().Trim();
                row.ProductNO = ushort.Parse(reader[3].ToString().Trim());
                row.ColourType = char.Parse(reader[4].ToString().Trim());
                row.ColorName = reader[5].ToString().Trim();
                row.ModelName = reader[6].ToString().Trim();

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
        public static CatalogContent GetCatalog(string id)
        {
            using DBConnection sqlClient = new();

            CatalogContent content = new();
            content.CatalogContents = new();
            content.CatalogTitles = new();

            string query = $"SELECT TOP 1 cat.catalogNo, my.modelYears, v.modelTypeCode, v.productNo, v.colorType, v.colorName, mk.modelName FROM Cataloge AS cat JOIN Variants AS v ON cat.VariantId = v.Id JOIN ModelYears AS my ON v.YearsId = my.Id JOIN Models AS mk ON mk.Id = my.modelId WHERE v.Id = {id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader titleReader = command.ExecuteReader();

            while (titleReader.Read())
            {
                content.CatalogTitles.CatalogNo = titleReader[0].ToString().Trim();
                content.CatalogTitles.ModelYears = Convert.ToUInt16(titleReader[1].ToString().Trim());
                content.CatalogTitles.ModelTypeCode = titleReader[2].ToString().Trim();
                content.CatalogTitles.ProductNo = Convert.ToUInt16(titleReader[3].ToString().Trim());
                content.CatalogTitles.ColorType = char.Parse(titleReader[4].ToString().Trim());
                content.CatalogTitles.ColourName = titleReader[5].ToString().Trim();
            }
            titleReader.Close();

            query = $"SELECT cat.Id, cat.figName, cat.figNo FROM Cataloge AS cat JOIN Variants AS v ON cat.VariantId = v.Id JOIN ModelYears AS my ON v.YearsId = my.Id JOIN Models AS mk ON mk.Id = my.modelId WHERE v.Id = {id}";

            command = new(query, sqlClient.sqlConnection);

            SqlDataReader partsReader = command.ExecuteReader();

            while (partsReader.Read())
            {
                PartsPosition row = new();

                row.Id = Convert.ToUInt32(partsReader[0].ToString().Trim());
                row.FigName = partsReader[1].ToString().Trim();
                row.FigNo = Convert.ToByte(partsReader[2].ToString().Trim());

                content.CatalogContents.Add(row);
            }
            partsReader.Close();

            return content; 
        }

        /// <summary>
        /// Получение информации касательно конкретной запчасти в формате JSON по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Part> GetPart(string id)
        {
            using DBConnection sqlClient = new();

            List<Part> content = new();

            string query = $"SELECT refNo,partNo,quantity,remarks,partName FROM Parts  WHERE CatalogeId = {id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Part row = new();

                row.RefNo = Convert.ToByte(reader[0].ToString().Trim());
                row.PartNo = reader[1].ToString().Trim();
                row.Quantity = Convert.ToByte(reader[2].ToString().Trim());
                row.Remarks = reader[3].ToString().Trim();
                row.PartName = reader[4].ToString().Trim();

                content.Add(row);
            }
            reader.Close();

            return content;
        }
    }
}
