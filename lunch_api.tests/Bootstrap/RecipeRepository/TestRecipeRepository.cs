using System.Collections.Generic;
using recipes_api.Dal;
using recipes_api.views;

namespace lunch_api.Tests.Bootstrap.RecipeRepository
{
    public class TestRecipeRepository : IRecipeRepository
    {
        private List<RecipeView> lsRecipes;

        public TestRecipeRepository(List<RecipeView> lsRecipes)
        {
            this.lsRecipes = lsRecipes;
        }
        public List<RecipeView> GetRecipeInventory()
        {
            return lsRecipes;
        }
    }
}