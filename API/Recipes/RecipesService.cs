using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Model.Repository;
using ViewRecipes = View.Recipes;
using ModelRecipes = Model.Recipes;

namespace API.Recipes
{
    public sealed class RecipesService
    {
        private readonly IRecipesRepository recipesRepository;
        private readonly IMapper mapper;

        public RecipesService(IRecipesRepository recipesRepository, IMapper mapper)
        {
            this.recipesRepository = recipesRepository ?? throw new ArgumentNullException(nameof(recipesRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ViewRecipes.Recipe> GetRecipeAsync(string id, CancellationToken token)
        {
            var modelRecipe = await this.recipesRepository.GetRecipeAsync(id, token);

            var viewRecipe = this.mapper.Map<ModelRecipes.Recipe, ViewRecipes.Recipe>(modelRecipe);
            return viewRecipe;
        }

        public async Task<ViewRecipes.RecipesList> SearchRecipesAsync(
            ViewRecipes.RecipeSearchInfo viewSearchInfo, CancellationToken token)
        {
            var modelSearchInfo = this.mapper
                .Map<ViewRecipes.RecipeSearchInfo, ModelRecipes.RecipeSearchInfo>(viewSearchInfo);
            var modelRecipesList = await this.recipesRepository.SearchRecipeAsync(modelSearchInfo, token);

            var viewRecipesList = this.mapper.Map<ModelRecipes.RecipesList, ViewRecipes.RecipesList>(modelRecipesList);
            return viewRecipesList;
        }

        public async Task<ViewRecipes.Recipe> CreateRecipeAsync(
            ViewRecipes.RecipeCreateInfo viewCreateInfo, CancellationToken token)
        {
            var modelCreateInfo = this.mapper
                .Map<ViewRecipes.RecipeCreateInfo, ModelRecipes.RecipeCreateInfo>(viewCreateInfo);
            ValidateOnCreate(modelCreateInfo);
            var modelRecipe = await this.recipesRepository.CreateRecipeAsync(modelCreateInfo, token);

            var viewRecipe = this.mapper.Map<ModelRecipes.Recipe, ViewRecipes.Recipe>(modelRecipe);
            return viewRecipe;
        }

        public async Task UpdateRecipeAsync(string id,
            ViewRecipes.RecipeUpdateInfo viewUpdateInfo, CancellationToken token)
        {
            var modelUpdateInfo = this.mapper
                .Map<ViewRecipes.RecipeUpdateInfo, ModelRecipes.RecipeUpdateInfo>(viewUpdateInfo);
            ValidateOnUpdate(modelUpdateInfo);
            await this.recipesRepository.UpdateRecipeAsync(id, modelUpdateInfo, token);
        }

        public async Task DeleteRecipeAsync(string id, CancellationToken token)
        {
            await this.recipesRepository.DeleteRecipeAsync(id, token);
        }

        public static void ValidateOnCreate(ModelRecipes.RecipeCreateInfo createInfo)
        {
        }

        public static void ValidateOnUpdate(ModelRecipes.RecipeUpdateInfo updateInfo)
        {
        }
    }
}