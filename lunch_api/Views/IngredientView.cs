using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace recipes_api.views
{
    public class IngredientListView
    {
        public IEnumerable<IngredientView> Ingredients { get; set; }
    }
    public class IngredientView
    {
        public string Title { get; set; }

        [JsonPropertyName("best-before")]
        public string BestBefore { get; set; }

        [JsonIgnore]
        public DateTime BestBeforeDate => Convert.ToDateTime(BestBefore);
        [JsonPropertyName("use-by")]
        public string UseBy { get; set; }
        [JsonIgnore]
        public DateTime UseByDate => Convert.ToDateTime(UseBy);
    }
}