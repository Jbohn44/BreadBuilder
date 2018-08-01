using BreadBuilder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.ViewModels
{
    public class AddIngredientViewModel
    {
        [Required]
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        public IList<Ingredient> ingredients = new List<Ingredient>();  // Hopefully this will work as a list that is bound to the ingredient viewmodel so I can pass a list of ingredients into the view to be displayed
                                                                        // Not working... need to figure out how to store and transfer a list of ingredients.  Probably could circumvent this issue with a data base... or possibly something with IngredientData.
    }
}

