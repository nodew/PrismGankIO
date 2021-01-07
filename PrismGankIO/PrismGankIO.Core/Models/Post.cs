using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PrismGankIO.Core.Models
{
    public class Post
    {
        public string Title { get; set; }

        [JsonPropertyName("desc")]
        public string Description { get; set; }

        public string Url { get; set; }

        public List<string> Images { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public int LikeCount { get; set; }

        public int Stars { get; set; }

        public int Views { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
