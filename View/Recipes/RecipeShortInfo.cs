using System;
using System.Text.Json.Serialization;

namespace View.Recipes
{
    public class RecipeShortInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("cuisine")]
        public string Cuisine { get; set; }
        
        [JsonPropertyName("category")]
        public string Category { get; set; }
        
        [JsonPropertyName("cookingTime")]
        public string CookingTime { get; set; }
        
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}