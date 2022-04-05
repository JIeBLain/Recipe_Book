using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using RecipesBook.Recipes;
using View.Recipes;

namespace RecipesBook.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService recipeService;

        public RecipesController(IRecipesService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id, CancellationToken token)
        {
            try
            {
                var recipe = await this.recipeService.GetRecipeAsync(id, token).ConfigureAwait(false);
                return this.Ok(recipe);
            }
            catch (RecipeNotFoundException)
            {
                return this.NotFound();
            }
        }

        [HttpGet("")]
        public async Task<ActionResult> SearchAsync([FromQuery] RecipeSearchInfo searchInfo, CancellationToken token)
        {
            var recipesList = await this.recipeService.SearchRecipesAsync(searchInfo, token).ConfigureAwait(false);
            return this.Ok(recipesList);
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateAsync([FromBody] RecipeCreateInfo createInfo, CancellationToken token)
        {
            try
            {
                var recipe = await this.recipeService.CreateRecipeAsync(createInfo, token).ConfigureAwait(false);
                return this.Ok(recipe);
            }
            catch (ValidationException e)
            {
                return this.BadRequest(e.ValidationResult);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAsync(string id,
            [FromBody] RecipeUpdateInfo updateInfo,
            CancellationToken token)
        {
            try
            {
                await this.recipeService.UpdateRecipeAsync(id, updateInfo, token).ConfigureAwait(false);
                return this.NoContent();
            }
            catch (ValidationException e)
            {
                return this.BadRequest(e.ValidationResult);
            }
            catch (RecipeNotFoundException)
            {
                return this.NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id, CancellationToken token)
        {
            await this.recipeService.DeleteRecipeAsync(id, token).ConfigureAwait(false);
            return this.NoContent();
        }

        [HttpDelete("")]
        public async Task<ActionResult> DeleteAllAsync(CancellationToken token)
        {
            await this.recipeService.DeleteAllRecipesAsync(token).ConfigureAwait(false);
            return this.NoContent();
        }
    }
}