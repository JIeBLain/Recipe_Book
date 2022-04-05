using System.Threading;
using System.Threading.Tasks;
using ViewRecipes = View.Recipes;

namespace RecipesBook.Recipes
{
    public interface IRecipesService
    {
        public Task<ViewRecipes.Recipe> GetRecipeAsync(string id, CancellationToken token);

        public Task<ViewRecipes.RecipesList> SearchRecipesAsync(
            ViewRecipes.RecipeSearchInfo viewSearchInfo, CancellationToken token);

        public Task<ViewRecipes.Recipe> CreateRecipeAsync(
            ViewRecipes.RecipeCreateInfo viewCreateInfo, CancellationToken token);

        public Task UpdateRecipeAsync(string id,
            ViewRecipes.RecipeUpdateInfo viewUpdateInfo, CancellationToken token);

        public Task DeleteRecipeAsync(string id, CancellationToken token);

        public Task DeleteAllRecipesAsync(CancellationToken token);
    }
}