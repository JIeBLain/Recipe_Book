using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace View.Recipes
{
    public sealed class Recipe : RecipeShortInfo
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("ingredients")]
        public IReadOnlyList<string> Ingredients { get; set; }
        
        [JsonPropertyName("directions")]
        public IReadOnlyList<string> Directions { get; set; }
    }
}