using System.Collections.Generic;
using recipes_api.views;
using System.Linq;
using System;
using recipes_api.Dal;

namespace recipes_api.Services
{
    public interface IRecipeService
    {
        List<RecipeView> GetRecipes();
    }

    public class RecipeService: IRecipeService
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IIngredientRepository ingredientRepository;

        public RecipeService(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository)
        {
            this.recipeRepository = recipeRepository;
            this.ingredientRepository = ingredientRepository;
        }
        public List<RecipeView> GetRecipes()
        {
            var recipeInventory = recipeRepository.GetRecipeInventory();
            var ingredientInventory = ingredientRepository.GetIngredientInventory();

            var ingredients = ingredientInventory.Where(i => i.UseByDate >= DateTime.Now);
            if(!ingredients.Any()) return null;
           
            var results = (from i in ingredients
            from r in recipeInventory
            where r.Ingredients.Any(w => w.Contains(i.Title))
            orderby i.BestBeforeDate descending
            select r).Distinct().ToList();
            
            return results;
        }
    }
}