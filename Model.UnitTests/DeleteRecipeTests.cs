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
    public class DeleteRecipeTests
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
        public async Task ThrowRecipeNotFoundException_WhenGetDeleteRecipe()
        {
            var recipe = this.recipesRepository.CreateRecipeAsync(new RecipeCreateInfo(), default).Result;

            this.recipesRepository.DeleteRecipeAsync(recipe.Id, default).Wait();

            Func<Task> action = async () => await this.recipesRepository
                .GetRecipeAsync(recipe.Id, default);

            await action.Should().ThrowAsync<RecipeNotFoundException>();
        }

        [Test]
        public async Task ThrowRecipeNotFoundException_WhenRecipeNotFound()
        {
            Func<Task> action = async () => await this.recipesRepository
                .DeleteRecipeAsync(Guid.NewGuid().ToString(), default);

            await action.Should().ThrowAsync<RecipeNotFoundException>();
        }
    }
}