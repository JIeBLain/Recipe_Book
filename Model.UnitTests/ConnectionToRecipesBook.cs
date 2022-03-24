using Model.Recipes;
using MongoDB.Driver;

namespace Model.UnitTests
{
    public class ConnectionToRecipesBook
    {
        public ConnectionToRecipesBook()
        {
            var connectionString = "mongodb://localhost:27017/recipesBook";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(connection.DatabaseName);
            Recipes = database.GetCollection<Recipe>("recipes");
        }
        
        public IMongoCollection<Recipe> Recipes { get; }
    }
}