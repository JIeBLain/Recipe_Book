using System.Collections.Generic;

namespace View.Recipes
{
    public sealed class Recipe : RecipeShortInfo
    {
        public string Description { get; set; }
        public IReadOnlyList<string> Ingredients { get; set; }
        public IReadOnlyList<string> Directions { get; set; }
    }
}