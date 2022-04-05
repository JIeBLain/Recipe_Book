using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Client.Models;

namespace Client.Tests
{
    public class DeleteAllRecipesAsyncTests
    {
        private IRecipesBookClient recipesBookClient;
        private string existsId;
        private string doesNotExistId = "c6e3d29f-ae7f-45e1-80f1-149c1e6gcb52";

        [OneTimeSetUp]
        public async Task Setup()
        {
            recipesBookClient = new RecipesBookClient();
            await recipesBookClient.DeleteAllRecipesAsync();
            var recipesCreateInfo = TestData.RecipesCreateInfo;
            foreach (var createInfo in recipesCreateInfo)
            {
                await recipesBookClient.CreateRecipeAsync(new RecipeCreateInfo
                {
                    Name = createInfo.Name,
                    Cuisine = createInfo.Cuisine,
                    Category = createInfo.Category,
                    Description = createInfo.Description,
                    Ingredients = createInfo.Ingredients,
                    Directions = createInfo.Directions,
                    CookingTime = createInfo.CookingTime
                });
            }
        }

        [Test]
        public async Task DeleteAllRecipesAsync_ShouldReturnNoContentResult_WhenRecipesExistOrNot()
        {
            var actual = await recipesBookClient.DeleteAllRecipesAsync();

            actual.StatusCode.Should().Be(204);
        }
    }
}