using System;
using System.Threading.Tasks;
using FluentAssertions;
using Model.Exceptions;
using Model.Repository;
using NUnit.Framework;

namespace Model.UnitTests
{
    [TestFixture]
    public class GetRecipeTests
    {
        private IRecipesRepository firstRecipesRepository;
        private IRecipesRepository secondRecipesRepository;

        [SetUp]
        public void Setup()
        {
            var recipesCollection = new ConnectionToRecipesBook().Recipes;
            recipesCollection.Database.DropCollection("recipes");
            this.firstRecipesRepository = new RecipesRepository(recipesCollection);
            this.secondRecipesRepository = new RecipesRepository(recipesCollection);
        }

        [Test]
        public void GotFromSecondRepository_WhenCreateInFirst()
        {
            var createInfo = new InitialRecipesTestData().Brownie;

            var expected = this.firstRecipesRepository.CreateRecipeAsync(createInfo, default).Result;

            var actual = this.secondRecipesRepository.GetRecipeAsync(expected.Id, default).Result;

            actual.Id.Should().Be(expected.Id);
            actual.Name.Should().Be(expected.Name);
            actual.Cuisine.Should().Be(expected.Cuisine);
            actual.Category.Should().Be(expected.Category);
            actual.Description.Should().Be(expected.Description);
            actual.Ingredients.Should().BeEquivalentTo(expected.Ingredients);
            actual.Directions.Should().BeEquivalentTo(expected.Directions);
            actual.CookingTime.Should().Be(expected.CookingTime);
            actual.CreatedAt.Should().BeWithin(TimeSpan.FromMilliseconds(100)).Before(expected.CreatedAt);
        }

        [Test]
        public async Task ThrowRecipeNotFoundException_WhenRecipeNotFound()
        {
            Func<Task> action = async () => await this.firstRecipesRepository
                .GetRecipeAsync(Guid.NewGuid().ToString(), default);

            await action.Should().ThrowAsync<RecipeNotFoundException>();
        }
    }
}