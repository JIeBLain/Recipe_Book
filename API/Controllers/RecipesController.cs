using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using API.Recipes;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using View.Recipes;

namespace API.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService recipeService;

        public RecipesController(RecipesService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id, CancellationToken token)
        {
            try
            {
                var recipe = await this.recipeService.GetRecipeAsync(id, token);
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
                var recipe = await this.recipeService.CreateRecipeAsync(createInfo, token);
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
                await this.recipeService.UpdateRecipeAsync(id, updateInfo, token);
                return this.Ok();
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
            try
            {
                await this.recipeService.DeleteRecipeAsync(id, token);
                return this.NoContent();
            }
            catch (RecipeNotFoundException)
            {
                return this.NotFound();
            }
        }
    }
}