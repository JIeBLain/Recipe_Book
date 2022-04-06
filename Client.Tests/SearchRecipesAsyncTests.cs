using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using View.Recipes;

namespace Client.Tests
{
    public class SearchRecipesAsyncTests
    {
        private IRecipesBookClient recipesBookClient;

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
        public async Task SearchRecipeAsync_ShouldReturnOneRecipe_WhenRecipeNameIsBrownie()
        {
            var expectedRecipe = TestData.Brownie;
            var shortInfo = new RecipeShortInfo
            {
                Name = expectedRecipe.Name,
                Cuisine = expectedRecipe.Cuisine,
                Category = expectedRecipe.Category,
                CookingTime = expectedRecipe.CookingTime,
                CreatedAt = expectedRecipe.CreatedAt
            };
            var expectedRecipesList = new RecipesList {Recipes = new List<RecipeShortInfo> {shortInfo}, Total = 1};

            var actual =
                await recipesBookClient.SearchRecipesAsync(new RecipeSearchInfo {Name = TestData.Brownie.Name});
            var recipesList = actual.Response;

            recipesList.Total.Should().Be(expectedRecipesList.Total);
        }

        [Test]
        public async Task SearchRecipeAsync_ShouldNotReturnRecipe_WhenRecipeDoesNotExist()
        {
            var expectedRecipesList = new RecipesList {Recipes = null, Total = 0};

            var actual = await recipesBookClient.SearchRecipesAsync(new RecipeSearchInfo {Name = "Борщ"});

            actual.StatusCode.Should().Be(200);
            actual.Response.Total.Should().Be(expectedRecipesList.Total);
            actual.Response.Recipes.Should().BeEquivalentTo(new List<RecipeShortInfo>());
        }

        [Test]
        public async Task SearchRecipeAsync_ShouldReturnFiveRecipes_WhenRecipesLimitIsStandard()
        {
            var expectedRecipesList = new RecipesList {Total = 5};

            var actual = await recipesBookClient.SearchRecipesAsync(new RecipeSearchInfo());

            actual.StatusCode.Should().Be(200);
            actual.Response.Total.Should().Be(expectedRecipesList.Total);
        }

        [Test]
        public async Task SearchRecipeAsync_ShouldReturnSixRecipes_WhenRecipesLimitIsSix()
        {
            var expectedRecipesList = new RecipesList {Total = 6};

            var actual = await recipesBookClient.SearchRecipesAsync(new RecipeSearchInfo {Limit = 6});

            actual.StatusCode.Should().Be(200);
            actual.Response.Total.Should().Be(expectedRecipesList.Total);
        }
    }
}