using System;
using System.Threading.Tasks;
using FluentAssertions;
using Model.Exceptions;
using Model.Recipes;
using Model.Repository;
using NUnit.Framework;

namespace Model.UnitTests
{
    [TestFixture]
    public class UpdateRecipeTests
    {
        private IRecipesRepository recipesRepository;

        [SetUp]
        public void Setup()
        {
            var recipesCollection = new ConnectionToRecipesBook().Recipes;
            recipesCollection.Database.DropCollection("recipes");
            this.recipesRepository = new RecipesRepository(recipesCollection);
        }

        [Test]
        public void OneFieldUpdate_WhenOneFieldNotNull()
        {
            var createInfo = new InitialRecipesTestData().Brownie;

            var recipe = this.recipesRepository.CreateRecipeAsync(createInfo, default).Result;
            var updateInfo = new RecipeUpdateInfo {CookingTime = "45"};

            this.recipesRepository.UpdateRecipeAsync(recipe.Id, updateInfo, default).Wait();

            var updatedRecipe = this.recipesRepository.GetRecipeAsync(recipe.Id, default).Result;
            updatedRecipe.Name.Should().Be(recipe.Name);
            updatedRecipe.Cuisine.Should().Be(recipe.Cuisine);
            updatedRecipe.Category.Should().Be(recipe.Category);
            updatedRecipe.Description.Should().Be(recipe.Description);
            updatedRecipe.Ingredients.Should().BeEquivalentTo(recipe.Ingredients);
            updatedRecipe.Directions.Should().BeEquivalentTo(recipe.Directions);
            updatedRecipe.CookingTime.Should().Be(updateInfo.CookingTime);
        }

        [Test]
        public void AllFieldsUpdate_WhenAllFieldsNotNull()
        {
            var createInfo = new InitialRecipesTestData().Brownie;
            var recipe = this.recipesRepository.CreateRecipeAsync(createInfo, default).Result;
            var updateInfo = new InitialRecipesTestData().CheesecakeFromCottageCheese;

            this.recipesRepository.UpdateRecipeAsync(recipe.Id, updateInfo, default).Wait();

            var updatedRecipe = this.recipesRepository.GetRecipeAsync(recipe.Id, default).Result;
            updatedRecipe.Name.Should().Be(updateInfo.Name);
            updatedRecipe.Cuisine.Should().Be(updateInfo.Cuisine);
            updatedRecipe.Category.Should().Be(updateInfo.Category);
            updatedRecipe.Description.Should().Be(updateInfo.Description);
            updatedRecipe.Ingredients.Should().BeEquivalentTo(updateInfo.Ingredients);
            updatedRecipe.Directions.Should().BeEquivalentTo(updateInfo.Directions);
            updatedRecipe.CookingTime.Should().Be(updateInfo.CookingTime);
        }

        [Test]
        public async Task ThrowRecipeNotFoundException_WhenRecipeNotFound()
        {
            Func<Task> action = async () => await this.recipesRepository.UpdateRecipeAsync(
                Guid.NewGuid().ToString(), new RecipeUpdateInfo {Name = "Banana pancakes"}, default);

            await action.Should().ThrowAsync<RecipeNotFoundException>();
        }
    }
}