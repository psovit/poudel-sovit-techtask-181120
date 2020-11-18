using System.Collections.Generic;
using recipes_api.Dal;
using recipes_api.views;

namespace lunch_api.Tests.Bootstrap.RecipeRepository
{
    public class TestIngredientRepository : IIngredientRepository
    {
        private List<IngredientView> lsIngredients;

        public TestIngredientRepository(List<IngredientView> lsIngredients)
        {
            this.lsIngredients = lsIngredients;
        }

        public List<IngredientView> GetIngredientInventory()
        {
            return lsIngredients;
        }
    }
}