using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Client.ClientResult;
using Client.Models;

namespace Client
{
    public class RecipesBookClient : IRecipesBookClient
    {
        private readonly HttpClient httpClient;

        public RecipesBookClient()
        {
            httpClient = new HttpClient {BaseAddress = new Uri("https://localhost:5001/")};
        }

        public async Task<ClientResult<Recipe>> GetRecipeAsync(string id)
        {
            var response = await httpClient.GetAsync($"recipes/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return GetClientResult<Recipe>(response, content);
        }

        public async Task<ClientResult<RecipesList>> SearchRecipesAsync(RecipeSearchInfo searchInfo)
        {
            var requestUri = new StringBuilder("recipes?");

            if (!string.IsNullOrWhiteSpace(searchInfo.Name))
                requestUri.Append($"name={searchInfo.Name}");

            if (!string.IsNullOrWhiteSpace(searchInfo.Cuisine))
                requestUri.Append($"&cuisine={searchInfo.Cuisine}");

            if (!string.IsNullOrWhiteSpace(searchInfo.Category))
                requestUri.Append($"&category={searchInfo.Category}");

            if (searchInfo.FromCreatedAt != null)
                requestUri.Append($"&fromCreatedAt={searchInfo.FromCreatedAt}");

            if (searchInfo.ToCreatedAt != null)
                requestUri.Append($"&toCreatedAt={searchInfo.ToCreatedAt}");

            if (searchInfo.Limit > 0)
                requestUri.Append($"&limit={searchInfo.Limit}");

            if (searchInfo.Offset >= 0)
                requestUri.Append($"&offset={searchInfo.Offset}");

            var response = await httpClient.GetAsync(requestUri.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return GetClientResult<RecipesList>(response, content);
        }

        public async Task<ClientResult.ClientResult> CreateRecipeAsync(RecipeCreateInfo createInfo)
        {
            var content = new StringContent(JsonSerializer.Serialize(createInfo), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("recipes/", content);
            return GetClientResult(response);
        }

        public async Task<ClientResult<Recipe>> UpdateRecipeAsync(string id, RecipeUpdateInfo updateInfo)
        {
            var content = JsonSerializer.Serialize(updateInfo);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PatchAsync($"recipes/{id}", stringContent);
            return GetClientResult<Recipe>(response, content);
        }

        public async Task<ClientResult.ClientResult> DeleteRecipeAsync(string id)
        {
            var response = await httpClient.DeleteAsync($"recipes/{id}");
            return GetClientResult(response);
        }

        public async Task<ClientResult.ClientResult> DeleteAllRecipesAsync()
        {
            var response = await httpClient.DeleteAsync("recipes");
            return GetClientResult(response);
        }

        private static ClientResult<TResponse> GetClientResult<TResponse>(HttpResponseMessage response, string content)
        {
            var statusCode = (int) response.StatusCode;
            return response.IsSuccessStatusCode
                ? new ClientResult<TResponse>(statusCode, JsonSerializer.Deserialize<TResponse>(content))
                : new ClientResult<TResponse>(statusCode, JsonSerializer.Deserialize<ClientError>(content));
        }

        private static ClientResult.ClientResult GetClientResult(HttpResponseMessage response)
        {
            var statusCode = (int) response.StatusCode;
            return response.IsSuccessStatusCode
                ? new ClientResult<Recipe>(statusCode)
                : new ClientResult<Recipe>(statusCode, new ClientError());
        }
    }
}