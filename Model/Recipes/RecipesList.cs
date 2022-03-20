using System.Collections.Generic;

namespace Model.Recipes
{
    public class RecipesList
    {
        public IReadOnlyList<Recipe> Recipes { get; set; }
        public long Total { get; set; }
    }
}