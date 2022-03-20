using System;

namespace Model.Recipes
{
    public class RecipeSearchInfo
    {
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}