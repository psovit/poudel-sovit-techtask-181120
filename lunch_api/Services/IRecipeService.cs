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

            var lsResults = new List<RecipeView>();

            if(recipeInventory == null || recipeInventory.Count == 0 || ingredientInventory == null || ingredientInventory.Count == 0) 
                return lsResults;

            foreach(var recipe in recipeInventory)
            {
                var hasExpiredIngredient = false;
                var hasIngredient = false;

                foreach(var recipeIngredient in recipe.Ingredients)
                {
                    var ingredient = ingredientInventory.FirstOrDefault(p => p.Title == recipeIngredient);
                    
                    if(ingredient == null) continue;
                    hasIngredient = true;

                    var isExpired = RecipeHasExpiredIngredient(recipe, ingredient);
                    if(isExpired)
                    {
                        hasExpiredIngredient = true;
                        break;
                    }
                }

                if(hasIngredient && !hasExpiredIngredient)
                {
                    lsResults.Add(recipe);
                }
            }

            return (from r in lsResults
              from i in ingredientInventory
              where i.UseByDate >= DateTime.Now
              orderby i.BestBeforeDate descending
              select r).Distinct().ToList();
        }

        private bool RecipeHasExpiredIngredient(RecipeView recipe, IngredientView ingredient)
        {
            if(recipe.Ingredients.Contains(ingredient.Title))
            {
                return ingredient.UseByDate < DateTime.Now;
            }
            return false;
        }
    }
}