using System;

namespace View.Recipes
{
    public class RecipeShortInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public string CookingTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}