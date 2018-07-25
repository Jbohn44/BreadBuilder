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
    }
}
