using System.Collections.Generic;

namespace recipes_api.views
{
    public class RecipeListView
    {
        public List<RecipeView> Recipes { get; set; }
    }
    public class RecipeView
    {
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
    }
}