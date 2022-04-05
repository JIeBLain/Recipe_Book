using System;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class RecipeSearchInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cuisine")]
        public string Cuisine { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("fromCreatedAt")]
        public DateTime? FromCreatedAt { get; set; }

        [JsonPropertyName("toCreatedAt")]
        public DateTime? ToCreatedAt { get; set; }

        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        [JsonPropertyName("offset")]
        public int? Offset { get; set; }
    }
}