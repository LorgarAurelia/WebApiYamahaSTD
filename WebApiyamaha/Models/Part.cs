using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiyamaha.Models
{
    public class Part
    {
        private string remarks;
        public byte RefNo { get; set; }
        public string PartNo { get; set; }
        public byte Quantity { get; set; }
        public string Remarks 
        {
            get { return remarks; } 
            set
            {
                if (value.Length < 2)
                    remarks = "null";
                else
                    remarks = value;
            }
        }
        public string PartName { get; set; }
    }
}
