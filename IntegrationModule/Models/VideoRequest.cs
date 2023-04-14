namespace IntegrationModule.Models
{
    public class VideoRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string GenreName { get; set; }
        public int TotalSeconds { get; set; }
        public string StreamingUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
 