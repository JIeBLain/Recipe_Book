using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class RecipeCreateInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("cuisine")]
        public string Cuisine { get; set; }
        
        [JsonPropertyName("category")]
        public string Category { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("ingredients")]
        public IReadOnlyList<string> Ingredients { get; set; }
        
        [JsonPropertyName("directions")]
        public IReadOnlyList<string> Directions { get; set; }
        
        [JsonPropertyName("cookingTime")]
        public string CookingTime { get; set; }
    }
}