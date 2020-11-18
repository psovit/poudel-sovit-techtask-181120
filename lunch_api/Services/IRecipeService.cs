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

            var lsResults = new List<RecipeWithBestBeforeDate>();

            if(recipeInventory == null || recipeInventory.Count == 0 || ingredientInventory == null || ingredientInventory.Count == 0) 
                return new List<RecipeView>();

            foreach(var recipe in recipeInventory)
            {
                var hasExpiredIngredient = false;
                var hasIngredient = false;
                DateTime bestBeforeCheck = DateTime.Now;

                foreach(var recipeIngredient in recipe.Ingredients)
                {
                    var ingredient = ingredientInventory.FirstOrDefault(p => p.Title == recipeIngredient);
                    
                    if(ingredient == null) continue;
                    hasIngredient = true;
                    if(ingredient.BestBeforeDate < bestBeforeCheck)
                    {
                        bestBeforeCheck = ingredient.BestBeforeDate;
                    }

                    var isExpired = RecipeHasExpiredIngredient(recipe, ingredient);
                    if(isExpired)
                    {
                        hasExpiredIngredient = true;
                        break;
                    }
                }

                if(hasIngredient && !hasExpiredIngredient)
                {
                    lsResults.Add(
                        new RecipeWithBestBeforeDate()
                        { 
                            Recipe = recipe,
                            BestBefore = bestBeforeCheck
                        }
                    );
                }
            }

            return lsResults.OrderByDescending(p => p.BestBefore).Select(p => p.Recipe).ToList();
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