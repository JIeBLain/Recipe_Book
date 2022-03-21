using System;
using System.Collections.Generic;

namespace Model.Recipes
{
    public class RecipeCreateInfo
    {
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public IReadOnlyList<string> Ingredients { get; set; }
        public IReadOnlyList<string> Directions { get; set; }
        public string CookingTime { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}