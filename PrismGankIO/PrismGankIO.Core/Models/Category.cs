using System.Text.Json.Serialization;

namespace PrismGankIO.Core.Models
{
    public enum Category
    {
        Article,
        Girl,
        GanHuo
    }

    public class SubType
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string CoverImageUrl { get; set; }

        [JsonPropertyName("desc")]
        public string Description { get; set; }
    }
}
