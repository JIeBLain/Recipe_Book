using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Client.Models;

namespace Client.Tests
{
    public class DeleteRecipeAsyncTests
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
            existsId = recipes.Response.Recipes.FirstOrDefault()?.Id;
        }

        [Test]
        public async Task DeleteRecipeAsync_ShouldReturnNoContentResult_WhenRecipeExists()
        {
            var actual = await recipesBookClient.DeleteRecipeAsync(existsId);

            actual.StatusCode.Should().Be(204);
        }

        [Test]
        public async Task DeleteRecipeAsync_ShouldReturnNoContentResult_WhenRecipeDoesNotExist()
        {
            var actual = await recipesBookClient.DeleteRecipeAsync(doesNotExistId);

            actual.StatusCode.Should().Be(204);
        }
    }
}