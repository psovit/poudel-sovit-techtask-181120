using lunch_api.Tests.Bootstrap.RecipeRepository;
using recipes_api.Services;
using Xunit;

namespace lunch_api.Tests 
{
    public class RecipeServiceTests
    {
        [Fact]
        public void EmptyRecipeInventorysReturnsEmptyResult()
        {
            var emptyIngredientRepo = new TestIngredientRepository(null);
            var emptyRecipeRepo = new TestRecipeRepository(null);
            var recipeService = new RecipeService(emptyRecipeRepo, emptyIngredientRepo);
            var result = recipeService.GetRecipes();

            Assert.Equal(result.Count, 0);
        }
    }
}