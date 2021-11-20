namespace WebApiyamaha.Services.SQL.Models
{
    public class SqlDataModel
    {
        private string id;
        private string sqlValue;
        private string image;

        public string Id { get => id; set => id = value.Trim(); }
        public string Value { get => sqlValue; set => sqlValue = value.Trim().Replace("+", "'"); }
        public string Image { get => image; set => image = value.Trim(); }
    }
}
