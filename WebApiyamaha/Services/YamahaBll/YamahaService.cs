using Json.Library;
using Microsoft.Extensions.Configuration;
using MiddlewareExceptionPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiyamaha.Services.SQL;
using WebApiyamaha.Services.SQL.Models;
using WebApiyamaha.Services.YamahaBll.DataWrapper;
using WebApiyamaha.Services.YamahaBll.Extensions;
using WebApiyamaha.Services.YamahaBll.Models;

namespace WebApiyamaha.Services.YamahaBll
{
    public class YamahaService
    {
        public static async Task<ServiceResponce<ComplexJson>> GetParametr(IConfiguration configuration, string idx = null)
        {
            string connectionString = configuration.GetConnectionString("DataBase");
            Parameter param = string.IsNullOrEmpty(idx)
                ? new Parameter()
                : idx.ToParameter();

            return _ = await DataController(connectionString, param);
        }

        public static async Task<ServiceResponce<List<PartComplexJson>>> GetParts(IConfiguration configuration, string idx)
        {
            string connectionString = configuration.GetConnectionString("DataBase");
            Parameter param = idx.ToParameter();

            return _ = await PartController(connectionString, param);
        }

        public static async Task<ServiceResponce<ComplexJson>> SearchParts(IConfiguration configuration, string searchArgument, string searchType)
        {
            if (!Enum.TryParse(searchType, out SearchType search))
                throw new ServiceException("Неподдерживаемый тип поиска", 400);

            searchType = search switch
            {
                SearchType.namePart => "partName",
                SearchType.numberPart => "partNo",
                _ => throw new ServiceException("Неподдерживаемый тип поиска", 400),
            };

            string connectionString = configuration.GetConnectionString("DataBase");
            Parameter param = new Parameter { Type = RouteType.Catalog };

            var sqlAnswer = await SqlService.SearchParts(searchArgument, searchType, connectionString);

            return _ = await DataWrapperService.ResponceParameter(sqlAnswer, "catalogOnSearch", "Search part in catalog", RouteType.Part, true, false);
        }

        private static async Task<ServiceResponce<ComplexJson>> DataController(string connectionString, Parameter param)
        {
            ServiceResponce<ComplexJson> responce = new();
            List<SqlDataModel> sqlData = new();
            switch (param.Type)
            {
                case RouteType.Categories:
                    sqlData = await SqlService.GetCategoriesAsync(connectionString);
                    responce = await DataWrapperService.ResponceParameter(sqlData);
                    break;
                case RouteType.Diseplacement:
                    sqlData = await SqlService.GetDiseplacement(connectionString, param.CurrentParameterId);
                    responce = await DataWrapperService.ResponceParameter(sqlData, "diseplacement", "Diseplacement", RouteType.Models);
                    break;
                case RouteType.Models:
                    sqlData = await SqlService.GetModelAsync(connectionString, param.CurrentParameterId);
                    responce = await DataWrapperService.ResponceParameter(sqlData, "models", "Models", RouteType.Years);
                    break;
                case RouteType.Years:
                    sqlData = await SqlService.GetYearsAsync(connectionString, param.CurrentParameterId);
                    responce = await DataWrapperService.ResponceParameter(sqlData, "years", "Models Years", RouteType.Variants);
                    break;
                case RouteType.Variants:
                    sqlData = await SqlService.GetModelsList(connectionString, param.CurrentParameterId);
                    responce = await DataWrapperService.ResponceParameter(sqlData, "variants", "Model's Colors", RouteType.Catalog, true, true);
                    break;
                case RouteType.Catalog:
                    sqlData = await SqlService.GetCatalog(connectionString, param.CurrentParameterId);
                    responce = await DataWrapperService.ResponceParameter(sqlData, "catalog", "Catalog", RouteType.Part, true, false);
                    break;
            }

            return responce;
        }

        private static async Task<ServiceResponce<List<PartComplexJson>>> PartController(string connectionString, Parameter param)
        {
            ServiceResponce<List<PartComplexJson>> responce = new();

            var DataConcreat = await SqlService.GetPart(connectionString, param.CurrentParameterId);
            responce = await DataWrapperService.ResponceParts(DataConcreat);

            return responce;
        }
    }
}
