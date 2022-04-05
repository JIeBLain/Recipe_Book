using System;
using System.ComponentModel.DataAnnotations;

namespace View.Recipes
{
    public class RecipeSearchInfo
    {
        public string Name { get; set; }

        public string Cuisine { get; set; }

        public string Category { get; set; }

        public DateTime? FromCreatedAt { get; set; }

        public DateTime? ToCreatedAt { get; set; }

        [Range(1, 25)]
        public int? Limit { get; set; } = 5;

        public int? Offset { get; set; } = 0;
    }
}