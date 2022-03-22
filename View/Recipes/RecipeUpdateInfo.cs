using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace View.Recipes
{
    [DataContract]
    public sealed class RecipeUpdateInfo
    {
        [DataMember]
        [StringLength(100)]
        public string Name { get; set; }
        
        [DataMember]
        [StringLength(100)]
        public string Cuisine { get; set; }
        
        [DataMember]
        [StringLength(100)]
        public string Category { get; set; }
        
        [DataMember]
        [StringLength(2500)]
        public string Description { get; set; }
        
        [DataMember]
        [MaxLength(50, ErrorMessage = "The field Ingredients must be a list type with a maximum length of '50")]
        public IReadOnlyList<string> Directions { get; set; }
        
        [DataMember]
        [MaxLength(20, ErrorMessage = "The field Directions must be a list type with a maximum length of '20")]
        public IReadOnlyList<string> Ingredients { get; set; }
        
        [DataMember]
        [StringLength(100)]
        public string CookingTime { get; set; }
    }
}