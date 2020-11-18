using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using recipes_api.views;
using recipes_api.Services;

namespace recipes_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LunchController : ControllerBase
    {
        private readonly IRecipeService recipeService;

        public LunchController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet]
        public List<RecipeView> Get()
        {
            return recipeService.GetRecipes();
        }
    }
}