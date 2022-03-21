using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace View.Recipes
{
    [DataContract]
    public class RecipeCreateInfo
    {
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string Name { get; set; }
        
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string Cuisine { get; set; }
        
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string Category { get; set; }
        
        [DataMember(IsRequired = true)]
        [StringLength(1000)]
        public string Description { get; set; }
        
        [DataMember(IsRequired = true)]
        [MaxLength(50, ErrorMessage = "The field Ingredients must be a list type with a maximum length of '50")]
        public IReadOnlyList<string> Ingredients { get; set; }
        
        [DataMember(IsRequired = true)]
        [MaxLength(20, ErrorMessage = "The field Directions must be a list type with a maximum length of '20")]
        public IReadOnlyList<string> Directions { get; set; }
        
        [DataMember(IsRequired = true)]
        [StringLength(100)]
        public string CookingTime { get; set; }
        
        [DataMember]
        public DateTime? CreatedAt { get; set; }
    }
}