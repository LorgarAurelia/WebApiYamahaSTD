namespace WebApiyamaha.Services.SQL.Models
{
    public class PartComponentsSql
    {
        private string partNo;
        private string quantity;
        private string remarks;
        private string partName;
        private string picName;

        public string PartNo { get => partNo; set => partNo = value.Trim(); }
        public string Quantity { get => quantity; set => quantity = value.Trim(); }
        public string Remarks { get => remarks; 
            set 
            { 
                remarks = value.Trim();

                if (string.IsNullOrEmpty(remarks))
                    remarks = null;
            } }
        public string PartName { get => partName; set => partName = value.Trim(); }
        public string ImageName { get => picName; set => picName = value.Trim(); }
    }
}
