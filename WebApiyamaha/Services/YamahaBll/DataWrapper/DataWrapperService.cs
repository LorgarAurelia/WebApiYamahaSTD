using Json.Library;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiyamaha.Services.SQL.Models;
using WebApiyamaha.Services.YamahaBll.Extensions;
using WebApiyamaha.Services.YamahaBll.Models;

namespace WebApiyamaha.Services.YamahaBll.DataWrapper
{
    public class DataWrapperService
    {
        public static async Task<ServiceResponce<ComplexJson>> ResponceWrapper(ComplexJson json)
        {
            ServiceResponce<ComplexJson> responce = new();
            responce.Data = json;
            return responce;
        }

        public static async Task<ServiceResponce<List<PartComplexJson>>> PartsWrapper(List<PartComplexJson> json)
        {
            ServiceResponce<List<PartComplexJson>> serviceResponce = new();
            serviceResponce.Data = json;
            return serviceResponce;
        }

        public static async Task<ServiceResponce<List<PartComplexJson>>> ResponceParts(List<CatalogSqlData> parts, List<PartComponentsSql> partsComponents, Parameter parameter)
        {
            List<PartComplexJson> jsonInList = new();
            PartComplexJson json = new();
            json.Parts = new();

            foreach (CatalogSqlData row in parts)
            {
                Parameter idxComponents = new();
                Part part = new();
                part.NextRoutes = new();

                idxComponents.CurrentParameterId = row.Id;
                idxComponents.Type = RouteType.Part;

                part.Title = row.FigName;
                part.Position = row.FigNo;
                part.NumberPartFormatted = row.IllustNo;
                part.Note = row.CatalogNo;
                part.NextRoutes.Add(new NextRoute { Idx = idxComponents.ToIdx(), Route = RoutePath.parts.ToString(), Description = "Components of parts" });

                json.Parts.Add(part);
            }

            jsonInList.Add(json);

            return _ = await PartsWrapper(jsonInList);
        }

        public static async Task<ServiceResponce<List<PartComplexJson>>> ResponceParts(List<PartComponentsSql> parts)
        {
            List<PartComplexJson> jsonInList = new();
            PartComplexJson json = new();
            json.Parts = new();
            string imageName = string.Empty;

            foreach (PartComponentsSql row in parts)
            {
                Part part = new();

                part.Title = row.PartName;
                part.NumberPartFormatted = row.PartNo;
                part.Note = row.Remarks;
                part.Quantity = row.Quantity;
                imageName = row.ImageName;

                json.Parts.Add(part);
            }

            json.Image = new Image { Path = imageName };
            jsonInList.Add(json);

            return _ = await PartsWrapper(jsonInList);
        }

        public static async Task<ServiceResponce<ComplexJson>> ResponceParameter(List<SqlDataModel> models, string key = "categories", string name = "Type of product", RouteType route = RouteType.Diseplacement, bool nextIsParts = false, bool withPicture = false)
        {
            ComplexJson json = new(key, name);

            foreach (SqlDataModel row in models)
            {
                Value jsonValue = new();
                Parameter idxComponents = new();

                idxComponents.CurrentParameterId = row.Id;
                idxComponents.Type = route;

                if (nextIsParts == false)
                    jsonValue.NextRoute = new NextRoute { Idx = idxComponents.ToIdx(), Route = RoutePath.parameter.ToString() };
                else jsonValue.NextRoute = new NextRoute { Idx = idxComponents.ToIdx(), Route = RoutePath.search.ToString() };

                if (withPicture == true)
                    jsonValue.Image = row.Image;

                jsonValue.Title = row.Value;

                json.Values.Add(jsonValue);
            }

            return _ = await ResponceWrapper(json);
        }
    }
}
