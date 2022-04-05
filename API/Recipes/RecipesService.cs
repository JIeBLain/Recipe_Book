using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Model.Repository;
using ViewRecipes = View.Recipes;
using ModelRecipes = Model.Recipes;

namespace RecipesBook.Recipes
{
    public sealed class RecipesService : IRecipesService
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

        public async Task DeleteAllRecipesAsync(CancellationToken token)
        {
            await this.recipesRepository.DeleteAllRecipesAsync(token);
        }

        private static void ValidateOnCreate(ModelRecipes.RecipeCreateInfo createInfo)
        {
            if (createInfo.Ingredients?.Any(string.IsNullOrWhiteSpace) == true)
            {
                throw new ValidationException("Ingredients cannot be null or whitespace.");
            }

            if (createInfo.Ingredients == null)
            {
                throw new ValidationException("Ingredients cannot be null.");
            }

            if (createInfo.Directions?.Any(string.IsNullOrWhiteSpace) == true)
            {
                throw new ValidationException("Directions cannot be null or whitespace.");
            }

            if (createInfo.Directions == null)
            {
                throw new ValidationException("Directions cannot be null.");
            }

            if (createInfo.Directions?.Count > createInfo.Directions?.Distinct().Count())
            {
                throw new ValidationException("All directions must be different!");
            }
        }

        private static void ValidateOnUpdate(ModelRecipes.RecipeUpdateInfo updateInfo)
        {
            if (string.IsNullOrWhiteSpace(updateInfo.Name) &&
                string.IsNullOrWhiteSpace(updateInfo.Cuisine) &&
                string.IsNullOrWhiteSpace(updateInfo.Category) &&
                string.IsNullOrWhiteSpace(updateInfo.Description) &&
                string.IsNullOrWhiteSpace(updateInfo.CookingTime) &&
                updateInfo.Directions == null &&
                updateInfo.Ingredients == null)
            {
                throw new ValidationException("Update info is empty.");
            }

            if (updateInfo.Ingredients?.Any(string.IsNullOrWhiteSpace) == true)
            {
                throw new ValidationException("Ingredients cannot be null or whitespace.");
            }

            if (updateInfo.Directions?.Any(string.IsNullOrWhiteSpace) == true)
            {
                throw new ValidationException("Directions cannot be null or whitespace.");
            }

            if (updateInfo.Directions?.Count > updateInfo.Directions?.Distinct().Count())
            {
                throw new ValidationException("All directions must be different!");
            }
        }
    }
}