using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiyamaha.Models
{
    public class CatalogContent
    {
        public CatalogTitle CatalogTitles { get; set; }

        public List<PartsPosition> CatalogContents { get; set; }
    }
}
