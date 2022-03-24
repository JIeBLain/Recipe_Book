using System;
using System.Threading;
using FluentAssertions;
using Model.Recipes;
using Model.Repository;
using NUnit.Framework;

namespace Model.UnitTests
{
    [TestFixture]
    public class SearchRecipesTests
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
        public void GotRecipesCreatedAfterDate_WhenFromCreatedAtNotEmpty()
        {
            var recipe20 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 20).ToUniversalTime()}, default).Result;
            var recipe24 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 24).ToUniversalTime()}, default).Result;
            var recipe31 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 31).ToUniversalTime()}, default).Result;

            var recipesList = this.recipesRepository.SearchRecipeAsync(new RecipeSearchInfo
                {FromCreatedAt = new DateTime(2022, 3, 24).ToUniversalTime()}, default).Result;

            recipesList.Recipes.Should().BeEquivalentTo(new[] {recipe24, recipe31});
        }

        [Test]
        public void GotRecipesCreatedBeforeDate_WhenToCreatedAtNotEmpty()
        {
            var recipe20 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 20).ToUniversalTime()}, default).Result;
            var recipe24 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 24).ToUniversalTime()}, default).Result;
            var recipe31 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 31).ToUniversalTime()}, default).Result;

            var recipesList = this.recipesRepository.SearchRecipeAsync(new RecipeSearchInfo
                {ToCreatedAt = new DateTime(2022, 3, 31).ToUniversalTime()}, default).Result;

            recipesList.Recipes.Should().BeEquivalentTo(new[] {recipe20, recipe24});
        }

        [Test]
        public void GotRecipesCreatedBetweenTwoDates_WhenFromCreatedAtAndToCreatedAtNotEmpty()
        {
            var recipe15 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 15).ToUniversalTime()}, default).Result;
            var recipe20 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 20).ToUniversalTime()}, default).Result;
            var recipe24 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 24).ToUniversalTime()}, default).Result;
            var recipe31 = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {CreatedAt = new DateTime(2022, 3, 31).ToUniversalTime()}, default).Result;

            var recipesList = this.recipesRepository.SearchRecipeAsync(new RecipeSearchInfo
            {
                FromCreatedAt = new DateTime(2022, 3, 17).ToUniversalTime(),
                ToCreatedAt = new DateTime(2022, 3, 25).ToUniversalTime()
            }, default).Result;

            recipesList.Recipes.Should().BeEquivalentTo(new[] {recipe20, recipe24});
        }

        [Test]
        public void GotTwoRecipesWithSameName_WhenNameNotEmpty()
        {
            var firstRecipe = this.recipesRepository
                .CreateRecipeAsync(new RecipeCreateInfo {Name = "Brownie"}, default).Result;
            var secondRecipe = this.recipesRepository
                .CreateRecipeAsync(new RecipeCreateInfo {Name = "Cheesecake"}, default).Result;
            var thirdRecipe = this.recipesRepository
                .CreateRecipeAsync(new RecipeCreateInfo {Name = "Brownie"}, default).Result;

            var recipesList = this.recipesRepository
                .SearchRecipeAsync(new RecipeSearchInfo {Name = "Brownie"}, default).Result;

            recipesList.Recipes.Should().HaveCount(2);
        }

        [Test]
        public void GotRecipeWithThreeProperties_WhenTheyNotEmpty()
        {
            var firstRecipe = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {Name = "Brownie", Cuisine = "USA", Category = "Dessert"}, default).Result;
            var secondRecipe = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {Name = "Borsch", Cuisine = "Russia", Category = "Soup recipes"}, default).Result;
            var thirdRecipe = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo
                {Name = "Borsch", Cuisine = "Ukraine", Category = "Soup recipes"}, default).Result;

            var recipesList = this.recipesRepository.SearchRecipeAsync(new RecipeSearchInfo
                {Name = "Borsch", Cuisine = "Russia", Category = "Soup recipes"}, default).Result;

            recipesList.Recipes.Should().HaveCount(1);
        }

        [Test]
        public void GotTenRecipes_WhenLimitIEmpty()
        {
            for (var i = 0; i < 20; i++)
            {
                this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo(), default).Wait();
            }

            var recipesList = this.recipesRepository
                .SearchRecipeAsync(new RecipeSearchInfo(), CancellationToken.None).Result;

            recipesList.Recipes.Should().HaveCount(10);
        }
    }
}