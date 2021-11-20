using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiyamaha.Services.YamahaBll.Models
{
    public class Parameter
    {
        public string ModelId { get; set; }
        public string CurrentParameterId { get; set; }
        public RouteType Type { get; set; } = 0;
    }

    public enum RouteType
    {
        Categories,
        Diseplacement,
        Models,
        Years,
        Variants,
        PartsCataloge,
        Part
    }

    public enum RoutePath
    {
        parameter,
        search,
        group,
        parts,
    }

    public enum SearchType
    {
        numberPart,
        namepart
    }
}
