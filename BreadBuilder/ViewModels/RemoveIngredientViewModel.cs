using BreadBuilder.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.ViewModels
{
    public class RemoveIngredientViewModel 
    {
        public IList<Ingredient> Ingredients { get; set; }

        public IList<Measurement> Measurements { get; set; }

        public IList<RecipeItem> RecipeItems { get; set; }
    }
}
