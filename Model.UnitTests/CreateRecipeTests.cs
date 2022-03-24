using System;
using FluentAssertions;
using NUnit.Framework;
using Model.Repository;

namespace Model.UnitTests
{
    [TestFixture]
    public class CreateRecipeTests
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
        public void GotCorrectRecipe_WhenCreate()
        {
            var createInfo = new InitialRecipesTestData().Brownie;

            var recipe = this.recipesRepository.CreateRecipeAsync(createInfo, default).Result;

            Guid.TryParse(recipe.Id, out _).Should().BeTrue();
            recipe.Name.Should().Be(createInfo.Name);
            recipe.Cuisine.Should().Be(createInfo.Cuisine);
            recipe.Category.Should().Be(createInfo.Category);
            recipe.Description.Should().Be(createInfo.Description);
            recipe.Ingredients.Should().BeEquivalentTo(createInfo.Ingredients);
            recipe.Directions.Should().BeEquivalentTo(createInfo.Directions);
            recipe.CookingTime.Should().Be(createInfo.CookingTime);
            recipe.CreatedAt.Should().BeWithin(TimeSpan.FromSeconds(1)).Before(DateTime.UtcNow);
        }
    }
}