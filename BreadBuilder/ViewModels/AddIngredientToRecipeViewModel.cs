using BreadBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.ViewModels
{
    public class AddIngredientToRecipeViewModel
    {

        public IList<Ingredient> ViewModelIngredients { get; set; }

        public AddIngredientToRecipeViewModel() { }
    }
}
