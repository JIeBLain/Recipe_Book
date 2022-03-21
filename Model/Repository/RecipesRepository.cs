using Model.Exceptions;
using Model.Recipes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly IMongoCollection<Recipe> recipesCollection;

        public RecipesRepository(IMongoCollection<Recipe> recipesCollection)
        {
            this.recipesCollection = recipesCollection ?? throw new ArgumentNullException(nameof(recipesCollection));
        }

        public async Task<Recipe> GetRecipeAsync(string id, CancellationToken token)
        {
            var recipe = await this.recipesCollection
                .Find(recipe => recipe.Id == id)
                .FirstOrDefaultAsync(token)
                .ConfigureAwait(false);

            if (recipe == null)
            {
                throw new RecipeNotFoundException(id);
            }

            return recipe;
        }

        public async Task<RecipesList> SearchRecipeAsync(RecipeSearchInfo searchInfo, CancellationToken token)
        {
            var builder = Builders<Recipe>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(searchInfo.Name))
            {
                filter &= builder.Eq(recipe => recipe.Name, searchInfo.Name);
            }

            if (!string.IsNullOrEmpty(searchInfo.Category))
            {
                filter &= builder.Eq(recipe => recipe.Category, searchInfo.Category);
            }

            if (!string.IsNullOrEmpty(searchInfo.Cuisine))
            {
                filter &= builder.Eq(recipe => recipe.Cuisine, searchInfo.Cuisine);
            }

            if (searchInfo.FromCreatedAt != null)
            {
                filter &= builder.Gte(recipe => recipe.CreatedAt, searchInfo.FromCreatedAt);
            }

            if (searchInfo.CreatedAt != null)
            {
                filter &= builder.Lt(recipe => recipe.CreatedAt, searchInfo.CreatedAt);
            }

            var cursor = this.recipesCollection.Find(filter);
            var recipes = await cursor
                .Limit(searchInfo.Limit)
                .Skip(searchInfo.Offset)
                .ToListAsync(token);
            var total = await cursor.CountDocumentsAsync(token);

            return new RecipesList { Recipes = recipes, Total = total };
        }

        public async Task<Recipe> CreateRecipeAsync(RecipeCreateInfo createInfo, CancellationToken token)
        {
            var recipe = new Recipe
            {
                Id = Guid.NewGuid().ToString(),
                Name = createInfo.Name,
                Cuisine = createInfo.Cuisine,
                Category = createInfo.Category,
                Description = createInfo.Description,
                Directions = createInfo.Directions,
                Ingredients = createInfo.Ingredients,
                CookingTime = createInfo.CookingTime,
                CreatedAt = createInfo.CreatedAt ?? DateTime.UtcNow
            };

            await this.recipesCollection.InsertOneAsync(recipe, cancellationToken: token);

            return recipe;
        }

        public async Task UpdateRecipeAsync(string id, RecipeUpdateInfo updateInfo, CancellationToken token)
        {
            var updates = new List<UpdateDefinition<Recipe>>();

            if (updateInfo.Name != null)
            {
                updates.Add(Builders<Recipe>.Update.Set(recipe => recipe.Name, updateInfo.Name));
            }

            if (updateInfo.Cuisine != null)
            {
                updates.Add(Builders<Recipe>.Update.Set(recipe => recipe.Cuisine, updateInfo.Cuisine));
            }

            if (updateInfo.Category != null)
            {
                updates.Add(Builders<Recipe>.Update.Set(recipe => recipe.Category, updateInfo.Category));
            }

            if (updateInfo.Description != null)
            {
                updates.Add(Builders<Recipe>.Update.Set(recipe => recipe.Description, updateInfo.Description));
            }

            if (updateInfo.Directions != null)
            {
                updates.Add(Builders<Recipe>.Update.Set(recipe => recipe.Directions, updateInfo.Directions));
            }

            if (updateInfo.Ingredients != null)
            {
                updates.Add(Builders<Recipe>.Update.Set(recipe => recipe.Ingredients, updateInfo.Ingredients));
            }

            if (updateInfo.CookingTime != null)
            {
                updates.Add(Builders<Recipe>.Update.Set(recipe => recipe.CookingTime, updateInfo.CookingTime));
            }

            var update = Builders<Recipe>.Update.Combine(updates);
            var updateResult = await this.recipesCollection
                .UpdateOneAsync(recipe => recipe.Id == id, update, cancellationToken: token)
                .ConfigureAwait(false);

            if (updateResult.ModifiedCount == 0)
            {
                throw new RecipeNotFoundException(id);
            }
        }

        public async Task DeleteRecipeAsync(string id, CancellationToken token)
        {
            var deleteResult = await this.recipesCollection.DeleteOneAsync(recipe => recipe.Id == id, token);

            if (deleteResult.DeletedCount == 0)
            {
                throw new RecipeNotFoundException(id);
            }
        }
    }
}