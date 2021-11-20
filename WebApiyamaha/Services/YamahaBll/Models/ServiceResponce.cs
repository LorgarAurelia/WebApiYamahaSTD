using System.Text.Json.Serialization;

namespace WebApiyamaha.Services.YamahaBll.Models
{
    public class ServiceResponce<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;

        [JsonIgnore]
        public int StatusCode { get; set; } = 200;
    }
}
