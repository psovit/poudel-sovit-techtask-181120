using System.Collections.Generic;
using lunch_api.Tests.Bootstrap.RecipeRepository;
using recipes_api.Services;
using recipes_api.views;
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

        [Fact]
        public void ExpiredUseByIngredientsAreNotIncludedInRecipes()
        {
            var useByExpiredIngredient = new IngredientView()
            {
                Title = "Ham",
                BestBefore = "2021-03-25",
                UseBy = "2020-01-01"
            };

            var useByNotExpiredIngredient = new IngredientView()
            {
                Title = "Cheese",
                BestBefore = "2021-03-25",
                UseBy = "2021-01-01"
            };

            var lsIngredients = new List<IngredientView>()
            {
                useByExpiredIngredient, useByNotExpiredIngredient
            };

            var recipeWExpired =  new RecipeView()
            {
                Title = "Ham & Cheese Toastie",
                Ingredients = new List<string>()
                {
                    "Ham",
                    "Cheese",
                    "Bread",
                    "Butter"
                }
            };

            var recipeWithoutExpired =  new RecipeView()
            {
                Title = "Cheese Toastie",
                Ingredients = new List<string>()
                {
                    "Cheese",
                    "Bread",
                    "Butter"
                }
            };

            var lsRecipes = new List<RecipeView>()
            {
                recipeWExpired, recipeWithoutExpired
            };

            var emptyIngredientRepo = new TestIngredientRepository(lsIngredients);
            var emptyRecipeRepo = new TestRecipeRepository(lsRecipes);
            var recipeService = new RecipeService(emptyRecipeRepo, emptyIngredientRepo);
            var result = recipeService.GetRecipes();

            Assert.Equal(result.Count, 1);
        }
    }
}