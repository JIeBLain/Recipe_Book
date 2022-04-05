using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Recipes;
using Model.Repository;
using MongoDB.Driver;

namespace RecipesBook.Recipes
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddRecipes(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration["ConnectionString"];
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("recipesBook");
                var collection = database.GetCollection<Recipe>("recipes");
                return collection;
            });
            services.AddSingleton<IRecipesRepository, RecipesRepository>();
            services.AddSingleton<IRecipesService, RecipesService>();
        }
    }
}