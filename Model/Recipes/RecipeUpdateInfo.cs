using System.Collections.Generic;

namespace Model.Recipes
{
    public class RecipeUpdateInfo
    {
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public IReadOnlyList<string> Directions { get; set; }
        public IReadOnlyList<string> Ingredients { get; set; }
        public string CookingTime { get; set; }
    }
}