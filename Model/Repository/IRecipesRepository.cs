using Model.Recipes;
using System.Threading;
using System.Threading.Tasks;

namespace Model.Repository
{
    public interface IRecipesRepository
    {
        public Task<Recipe> GetRecipeAsync(string id, CancellationToken token);

        public Task<RecipesList> SearchRecipeAsync(RecipeSearchInfo searchInfo, CancellationToken token);

        public Task<Recipe> CreateRecipeAsync(RecipeCreateInfo createInfo, CancellationToken token);

        public Task UpdateRecipeAsync(string id, RecipeUpdateInfo updateInfo, CancellationToken token);

        public Task DeleteRecipeAsync(string id, CancellationToken token);

        public Task DeleteAllRecipesAsync(CancellationToken token);
    }
}