using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiyamaha.Models
{
    public class ModelsList
    {
        private string name;

        public int Id { get; set; }
        public int ModelYears { get; set; }
        public string ModelTypeCode { get; set; }
        public ushort ProductNO { get; set; }
        public char ColourType { get; set; }
        public string ColorName { get; set; }
        public string ModelName { get { return name; } set { name = value.Trim().Replace("+", "'"); } }
    }
}
