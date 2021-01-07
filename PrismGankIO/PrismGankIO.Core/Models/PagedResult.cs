using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PrismGankIO.Core.Models
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; }

        public int Page { get; set; }

        [JsonPropertyName("page_count")]
        public int PageCount { get; set; }

        [JsonPropertyName("total_counts")]
        public int Total { get; set; }

        public int Status { get; set; }
    }
}
