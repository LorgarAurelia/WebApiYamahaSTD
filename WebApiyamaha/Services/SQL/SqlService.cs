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
        /// <summary>
        /// Метод отвечающий за три таблицы, по которым идёт первичная фильтрация.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private static string GetModelsInfo(string id = "0", string table = "empty")
        {
            using DBConnection sqlClient = new();

            List<ModelsInfo> content = new();

            string json;

            string query = string.Empty;


            switch (table)
            {
                case "empty":
                    query = "select ProductName, productId from Categories"; 
                    break;
                case "Diseplacement":
                case "displacement":
                    query = $"SELECT Displacement, id FROM Displacement where productId = {id}";
                    break;
                case "Model":
                    query = $"select dispModelName, id from Models where displacementID = {id}";
                    break;
                case "ModelYears":
                    query = $"select modelYears, id from ModelYears where modelId = {id}";
                    break;
            }

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

            json = JsonConvert.SerializeObject(content);
            return json;
        }

        /// <summary>
        /// Мотвед отвечающий за таблицу моделей по цветам.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string GetModelsList(string id)
        {
            string json = string.Empty;

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

            json = JsonConvert.SerializeObject(content);

            return json;
        }

        /// <summary>
        /// Метод получает Каталог в Формате JSON по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string GetCatalog(string id)
        {
            string json = string.Empty;

            using DBConnection sqlClient = new();

            CatalogTitle catalogTitle = new();
            List<PartsPosition> catalogContent = new();

            string query = $"SELECT TOP 1 cat.catalogNo, my.modelYears, v.modelTypeCode, v.productNo, v.colorType, v.colorName, mk.modelName FROM Cataloge AS cat JOIN Variants AS v ON cat.VariantId = v.Id JOIN ModelYears AS my ON v.YearsId = my.Id JOIN Models AS mk ON mk.Id = my.modelId WHERE v.Id = {id}";

            SqlCommand command = new(query, sqlClient.sqlConnection);

            SqlDataReader titleReader = command.ExecuteReader();

            while (titleReader.Read())
            {
                catalogTitle.CatalogNo = titleReader[0].ToString().Trim();
                catalogTitle.ModelYears = Convert.ToUInt16(titleReader[1].ToString().Trim());
                catalogTitle.ModelTypeCode = titleReader[2].ToString().Trim();
                catalogTitle.ProductNo = Convert.ToUInt16(titleReader[3].ToString().Trim());
                catalogTitle.ColorType = char.Parse(titleReader[4].ToString().Trim());
                catalogTitle.ColourName = titleReader[5].ToString().Trim();
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

                catalogContent.Add(row);
            }
            partsReader.Close();

            json = JsonConvert.SerializeObject(catalogTitle) + JsonConvert.SerializeObject(catalogContent);
            return json; 
        }

        /// <summary>
        /// Получение информации касательно конкретной запчасти в формате JSON по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string GetPart(string id)
        {
            string json = string.Empty;

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

            json = JsonConvert.SerializeObject(content);

            return json;
        }
        /// <summary>
        /// Метод сервиса определяющий к какой таблице БД необходимо получить доступ.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyForSearch"></param>
        /// <returns></returns>
        public static string GetFromDataBase(string table, string keyForSearch = "empty")
        {
            string json = string.Empty;

            try
            {
                int check = int.Parse(keyForSearch);
            }
            catch (Exception)
            {
                ModelsInfo CancelAccesToDbA = new();

                CancelAccesToDbA.Id = 0;
                CancelAccesToDbA.Value = "Error of query";

                json = JsonConvert.SerializeObject(CancelAccesToDbA);

                return json;
            }

            switch (table)
            {
                case "Categories":
                    json = GetModelsInfo();
                    break;
                case "Model":
                case "Diseplacement":
                case "ModelYears":
                    json = GetModelsInfo(keyForSearch, table);
                    break;
                case "ModelsList":
                    json = GetModelsList(keyForSearch);
                    break;
                case "Catalog":
                    json = GetCatalog(keyForSearch);
                    break;
                case "Part":
                    json = GetPart(keyForSearch);
                    break;
            }
            return json;
            
        }
    }
}
