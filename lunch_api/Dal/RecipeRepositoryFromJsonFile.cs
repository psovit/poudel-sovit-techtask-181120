using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using recipes_api.Utils;
using recipes_api.views;

namespace recipes_api.Dal
{
    public class RecipeRepositoryFromJsonFile : IRecipeRepository
    {
        private readonly IConfiguration configuration;
        private RecipeListView recipeListView;

        public RecipeRepositoryFromJsonFile(IConfiguration configuration)
        {
            this.configuration = configuration;
            recipeListView = ReadRecipesFromJson();
        }
        public List<RecipeView> GetRecipeInventory()
        {
            if(recipeListView == null)
            {
                recipeListView = ReadRecipesFromJson();
            }

            return recipeListView.Recipes;
        }

        private RecipeListView ReadRecipesFromJson()
        {
            var jsonPath = configuration.GetValue<string>("DataPaths:recipesJson");
            return JsonFileReader.Deserialize<RecipeListView>(jsonPath);
        }
    }
}