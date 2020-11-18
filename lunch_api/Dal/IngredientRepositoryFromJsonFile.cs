using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using recipes_api.Utils;
using recipes_api.views;

namespace recipes_api.Dal
{
    public class IngredientRepositoryFromJsonFile : IIngredientRepository
    {
        private readonly IConfiguration configuration;
        private IngredientListView ingredientListView;

        public IngredientRepositoryFromJsonFile(IConfiguration configuration)
        {
            this.configuration = configuration;
            ingredientListView = ReadIngredientsFromJson();
        }
        public List<IngredientView> GetIngredientInventory()
        {
            if(ingredientListView == null)
            {
                ingredientListView = ReadIngredientsFromJson();
            }

            return ingredientListView.Ingredients.ToList();
        }

        private IngredientListView ReadIngredientsFromJson()
        {
            var jsonPath = configuration.GetValue<string>("DataPaths:ingredientsJson");

            return JsonFileReader.Deserialize<IngredientListView>(jsonPath);
        }
    }
}