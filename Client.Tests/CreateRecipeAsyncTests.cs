using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using View.Recipes;

namespace Client.Tests
{
    public class CreateRecipeAsyncTests
    {
        private IRecipesBookClient recipesBookClient;

        [OneTimeSetUp]
        public async Task Setup()
        {
            recipesBookClient = new RecipesBookClient();
            await recipesBookClient.DeleteAllRecipesAsync();
        }

        [Test]
        public async Task CreateRecipeAsync_ShouldReturnOkObjectResult_WhenRecipeIsCreated()
        {
            var expected = TestData.Brownie;
            var createInfo = new RecipeCreateInfo
            {
                Name = expected.Name,
                Cuisine = expected.Cuisine,
                Category = expected.Category,
                Description = expected.Description,
                Ingredients = expected.Ingredients,
                Directions = expected.Directions,
                CookingTime = expected.CookingTime
            };

            var actual = await recipesBookClient.CreateRecipeAsync(createInfo);

            actual.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task CreateRecipeAsync_ShouldReturnBadRequestObjectResult_WhenRecipeIsNotCreated()
        {
            var expected = TestData.Brownie;
            var createInfo = new RecipeCreateInfo
            {
                Name = expected.Name,
                Cuisine = expected.Cuisine,
                Category = expected.Category,
                Description = expected.Description,
                Ingredients = null,
                Directions = expected.Directions,
                CookingTime = expected.CookingTime
            };

            var actual = await recipesBookClient.CreateRecipeAsync(createInfo);

            actual.StatusCode.Should().Be(400);
        }
    }
}