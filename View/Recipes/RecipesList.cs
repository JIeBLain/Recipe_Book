using System.Collections.Generic;

namespace View.Recipes
{
    public class RecipesList
    {
        public IReadOnlyList<RecipeShortInfo> Recipes { get; set; }
        public long Total { get; set; }
    }
}