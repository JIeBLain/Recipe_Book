using System;

namespace View.Recipes
{
    public class RecipeSearchInfo
    {
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
        public int? Limit { get; set; } = 100;
        public int? Offset { get; set; } = 0;
    }
}