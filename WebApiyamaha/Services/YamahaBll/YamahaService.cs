using Json.Library;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<ServiceResponce<List<PartComplexJson>>> GetParts (IConfiguration configuration, string idx)
        {
            string connectionString = configuration.GetConnectionString("DataBase");
            Parameter param = idx.ToParameter();

            return _ = await PartController(connectionString, param);
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
                    responce = await DataWrapperService.ResponceParameter(sqlData, "variants", "Model's Colors", RouteType.PartsCataloge, true, true);
                    break;
            }

            return responce;
        }

        private static async Task<ServiceResponce<List<PartComplexJson>>> PartController(string connectionString, Parameter param)
        {
            ServiceResponce<List<PartComplexJson>> responce = new();

            switch (param.Type)
            {
                case RouteType.PartsCataloge:
                    var sqlData = await SqlService.GetCatalog(connectionString, param.CurrentParameterId);
                    responce = await DataWrapperService.ResponceParts(sqlData, param);
                    break;
                case RouteType.Part:
                    var DataConcreat = await SqlService.GetPart(connectionString, param.CurrentParameterId);
                    responce = await DataWrapperService.ResponceParts(DataConcreat);
                    break;
            }

            return responce;
        }
    }
}
