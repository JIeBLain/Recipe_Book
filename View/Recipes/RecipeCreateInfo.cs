using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace View.Recipes
{
    [DataContract]
    public class RecipeCreateInfo
    {
        [JsonPropertyName("name")]
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string Name { get; set; }
        
        [JsonPropertyName("cuisine")]
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string Cuisine { get; set; }
        
        [JsonPropertyName("category")]
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string Category { get; set; }
        
        [JsonPropertyName("description")]
        [DataMember(IsRequired = true)]
        [StringLength(2500)]
        public string Description { get; set; }
        
        [JsonPropertyName("ingredients")]
        [DataMember(IsRequired = true)]
        [MaxLength(50, ErrorMessage = "The field Ingredients must be a list type with a maximum length of '50")]
        public IReadOnlyList<string> Ingredients { get; set; }
        
        [JsonPropertyName("directions")]
        [DataMember(IsRequired = true)]
        [MaxLength(20, ErrorMessage = "The field Directions must be a list type with a maximum length of '20")]
        public IReadOnlyList<string> Directions { get; set; }
        
        [JsonPropertyName("cookingTime")]
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string CookingTime { get; set; }
    }
}