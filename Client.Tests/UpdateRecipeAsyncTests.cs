using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Client.Models;

namespace Client.Tests
{
    public class UpdateRecipeAsyncTests
    {
        private IRecipesBookClient recipesBookClient;
        private string existsId;
        private string doesNotExistId = "c6e3d29f-ae7f-45e1-80f1-149c1e6gcb52";

        [OneTimeSetUp]
        public async Task Setup()
        {
            recipesBookClient = new RecipesBookClient();
            await recipesBookClient.DeleteAllRecipesAsync();

            var expected = TestData.Brownie;
            await recipesBookClient.CreateRecipeAsync(new RecipeCreateInfo
            {
                Name = expected.Name,
                Cuisine = expected.Cuisine,
                Category = expected.Category,
                Description = expected.Description,
                Ingredients = expected.Ingredients,
                Directions = expected.Directions,
                CookingTime = expected.CookingTime
            });
            var recipes = await recipesBookClient.SearchRecipesAsync(new RecipeSearchInfo {Name = expected.Name});
            var recipe = recipes.Response.Recipes.FirstOrDefault();
            existsId = recipe?.Id;
        }

        [Test]
        public async Task UpdateRecipeAsync_ShouldUpdateRecipeName_WhenRecipeExists()
        {
            var updateInfo = new RecipeUpdateInfo {Name = "Новый Брауни",};

            var actual = await recipesBookClient.UpdateRecipeAsync(existsId, updateInfo);

            actual.StatusCode.Should().Be(204);
        }

        [Test]
        public async Task UpdateRecipeAsync_ShouldNotUpdateRecipeName_WhenRecipeDoesNotExist()
        {
            var updateInfo = new RecipeUpdateInfo {Name = "Новый Брауни",};

            var actual = await recipesBookClient.UpdateRecipeAsync(doesNotExistId, updateInfo);

            actual.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task UpdateRecipeAsync_ShouldReturnBadRequestObjectResult_WhenRecipeDoesNotExist()
        {
            var updateInfo = new RecipeUpdateInfo {Directions = null};

            var actual = await recipesBookClient.UpdateRecipeAsync(doesNotExistId, updateInfo);

            actual.StatusCode.Should().Be(400);
        }
    }
}