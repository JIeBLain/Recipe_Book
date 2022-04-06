using System.Threading.Tasks;
using Client.ClientResult;
using View.Recipes;

namespace Client
{
    public interface IRecipesBookClient
    {
        public Task<ClientResult<Recipe>> GetRecipeAsync(string id);
        public Task<ClientResult<RecipesList>> SearchRecipesAsync(RecipeSearchInfo searchInfo);
        public Task<ClientResult.ClientResult> CreateRecipeAsync(RecipeCreateInfo createInfo);
        public Task<ClientResult<Recipe>> UpdateRecipeAsync(string id, RecipeUpdateInfo updateInfo);
        public Task<ClientResult.ClientResult> DeleteRecipeAsync(string id);
        public Task<ClientResult.ClientResult> DeleteAllRecipesAsync();
    }
}