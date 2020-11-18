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

            var ingredientRepo = new TestIngredientRepository(lsIngredients);
            var recipeRepo = new TestRecipeRepository(lsRecipes);
            var recipeService = new RecipeService(recipeRepo, ingredientRepo);
            var result = recipeService.GetRecipes();

            Assert.Equal(result.Count, 1);
        }

        [Fact]
        public void NoRecipesIfIngredientsDontMatchTest()
        {
            var ingredient1 = new IngredientView()
            {
                Title = "Ham",
                BestBefore = "2021-03-25",
                UseBy = "2021-01-01"
            };

            var ingredient2 = new IngredientView()
            {
                Title = "Cheese",
                BestBefore = "2021-03-25",
                UseBy = "2021-01-01"
            };

            var lsIngredients = new List<IngredientView>()
            {
                ingredient1, ingredient2
            };

            var recipe1 =  new RecipeView()
            {
                Title = "Bread Toastie",
                Ingredients = new List<string>()
                {
                    "Bread",
                    "Butter"
                }
            };

            var recipe2 =  new RecipeView()
            {
                Title = "Rice Toastie",
                Ingredients = new List<string>()
                {
                    "Rice",
                    "Butter"
                }
            };

            var lsRecipes = new List<RecipeView>()
            {
                recipe1, recipe2
            };

            var ingredientRepo = new TestIngredientRepository(lsIngredients);
            var recipeRepo = new TestRecipeRepository(lsRecipes);
            var recipeService = new RecipeService(recipeRepo, ingredientRepo);
            var result = recipeService.GetRecipes();

            Assert.Equal(result.Count, 0);
        }


        [Fact]
        public void GetRecipesIfIngredientsMatchTest()
        {
            var ingredient1 = new IngredientView()
            {
                Title = "Ham",
                BestBefore = "2021-03-25",
                UseBy = "2021-01-01"
            };

            var ingredient2 = new IngredientView()
            {
                Title = "Cheese",
                BestBefore = "2021-03-25",
                UseBy = "2021-01-01"
            };

            var lsIngredients = new List<IngredientView>()
            {
                ingredient1, ingredient2
            };

            var recipe1 =  new RecipeView()
            {
                Title = "Ham Toastie",
                Ingredients = new List<string>()
                {
                    "Ham",
                    "Bread",
                    "Butter"
                }
            };

            var recipe2 =  new RecipeView()
            {
                Title = "Rice Toastie",
                Ingredients = new List<string>()
                {
                    "Rice",
                    "Butter"
                }
            };

            var lsRecipes = new List<RecipeView>()
            {
                recipe1, recipe2
            };

            var ingredientRepo = new TestIngredientRepository(lsIngredients);
            var recipeRepo = new TestRecipeRepository(lsRecipes);
            var recipeService = new RecipeService(recipeRepo, ingredientRepo);
            var result = recipeService.GetRecipes();

            Assert.Equal(result.Count, 1);
        }

        [Fact]
        public void BestBeforeOrderingTest()
        {
            var ingredient1 = new IngredientView()
            {
                Title = "Ham",
                BestBefore = "2021-03-25",
                UseBy = "2021-01-01"
            };

            var ingredient2 = new IngredientView()
            {
                Title = "Cheese",
                BestBefore = "2020-01-01",
                UseBy = "2021-01-01"
            };

            var ingredient3 = new IngredientView()
            {
                Title = "Beans",
                BestBefore = "2021-04-01",
                UseBy = "2021-01-01"
            };

            var lsIngredients = new List<IngredientView>()
            {
                ingredient1, ingredient2, ingredient3
            };

            var recipe1 =  new RecipeView()
            {
                Title = "Ham Toastie",
                Ingredients = new List<string>()
                {
                    "Ham",
                    "Bread",
                    "Butter"
                }
            };

            var recipe2 =  new RecipeView()
            {
                Title = "Cheese Toastie",
                Ingredients = new List<string>()
                {
                    "Cheese",
                    "Bread",
                    "Butter"
                }
            };

            var recipe3 =  new RecipeView()
            {
                Title = "Beans Toastie",
                Ingredients = new List<string>()
                {
                    "Beans",
                    "Bread",
                    "Butter"
                }
            };

            var lsRecipes = new List<RecipeView>()
            {
                recipe1, recipe2, recipe3
            };

            var ingredientRepo = new TestIngredientRepository(lsIngredients);
            var recipeRepo = new TestRecipeRepository(lsRecipes);
            var recipeService = new RecipeService(recipeRepo, ingredientRepo);
            var result = recipeService.GetRecipes();

            var lastRecipe = result[2];
            Assert.Equal(recipe2, lastRecipe);
        }
    }
}