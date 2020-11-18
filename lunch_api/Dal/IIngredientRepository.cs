using System.Collections.Generic;
using recipes_api.views;

namespace recipes_api.Dal
{
    public interface IIngredientRepository
    {
        List<IngredientView> GetIngredientInventory();
    }
}