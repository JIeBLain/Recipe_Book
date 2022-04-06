using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace View.Recipes
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
        [Range(1, 25)]
        public int? Limit { get; set; } = 5;

        [JsonPropertyName("offset")]
        public int? Offset { get; set; } = 0;
    }
}