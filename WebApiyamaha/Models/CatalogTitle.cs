namespace WebApiyamaha.Models
{
    public class CatalogTitle
    {
        private string formatedYears;
        public string CatalogNo { get; set; }
        public ushort ModelYears { get; set; }
        public string ModelTypeCode { get; set; }
        public ushort ProductNo { get; set; }
        public char ColorType { get; set; }
        public string ColourName { get; set; }
        public string ModelName 
        { 
            get 
                { 
                return formatedYears; 
                } 
            set 
                { 
                    formatedYears = value.Trim().Replace("+", "'"); 
                } 
        }
    }
}
