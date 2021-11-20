using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiyamaha.Services.SQL.Models
{
    public class CatalogSqlData
    {
        private string figName;
        private string id;
        private string figNo;
        private string illustNo;
        private string catalogNo;
        private string pictureName;

        public string Id { get => id; set => id = value.Trim(); }
        public string FigName { get => figName; set => figName = value.Trim(); }
        public string FigNo { get => figNo; set => figNo = value.Trim(); }
        public string IllustNo { get => illustNo; set => illustNo = value.Trim(); }
        public string CatalogNo { get => catalogNo; set => catalogNo = $"Catalog № {value.Trim()}"; }
        public string PictureName { get => pictureName; set => pictureName = value.Trim(); }
    }
}
