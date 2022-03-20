using System;

namespace Model.Exceptions
{
    public class RecipeNotFoundException : Exception
    {
        public RecipeNotFoundException(string recipeId)
            : base($"Recipe with id {recipeId} not found.")
        {
        }
    }
}