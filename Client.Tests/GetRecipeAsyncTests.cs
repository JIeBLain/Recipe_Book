using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Client.Models;

namespace Client.Tests
{
    public class GetRecipeAsyncTests
    {
        private IRecipesBookClient recipesBookClient;
        private string existsId;
        private string doesNotExistId = "c6e3d29f-ae7f-45e1-80f1-149c1e6gcb52";
        private DateTime createdAt;

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
            if (recipe?.CreatedAt != null)
                createdAt = recipe.CreatedAt;
        }

        [Test]
        public async Task GetRecipeAsync_ShouldReturnRecipeById_WhenRecipeExists()
        {
            var expected = TestData.Brownie;

            var actual = await recipesBookClient.GetRecipeAsync(existsId);
            var recipe = actual.Response;

            actual.StatusCode.Should().Be(200);
            recipe.Id.Should().Be(existsId);
            recipe.Name.Should().Be(expected.Name);
            recipe.Cuisine.Should().Be(expected.Cuisine);
            recipe.Category.Should().Be(expected.Category);
            recipe.Description.Should().Be(expected.Description);
            recipe.Ingredients.Should().BeEquivalentTo(expected.Ingredients);
            recipe.Directions.Should().BeEquivalentTo(expected.Directions);
            recipe.CookingTime.Should().Be(expected.CookingTime);
            recipe.CreatedAt.Should().Be(createdAt);
        }

        [Test]
        public async Task GetRecipeAsync_ShouldReturnNotFoundResult_WhenRecipeDoesNotExist()
        {
            var actual = await recipesBookClient.GetRecipeAsync(doesNotExistId);

            actual.StatusCode.Should().Be(404);
        }
    }
}