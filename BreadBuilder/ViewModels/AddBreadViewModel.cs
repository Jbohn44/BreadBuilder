using BreadBuilder.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.ViewModels
{
    public class AddBreadViewModel
    {
        [Required]
        [Display(Name = "Bread Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }

        
        public IList<RecipeItem> RecipeItems { get; set; }


        public Measurement Measurement { get; set; }

        public Ingredient Ingredient { get; set; }

        public List<SelectListItem> MeasurementUnits { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public List<Measurement> Measurements { get; set; }
       
        public AddBreadViewModel()
        {

            MeasurementUnits = new List<SelectListItem>();
            
            
            MeasurementUnits.Add(new SelectListItem
            {
                Value = ((int)MeasurementUnit.oz).ToString(),
                Text = MeasurementUnit.oz.ToString()
            });

            MeasurementUnits.Add(new SelectListItem
            {
                Value = ((int)MeasurementUnit.g).ToString(),
                Text = MeasurementUnit.g.ToString()
            });

            MeasurementUnits.Add(new SelectListItem
            {
                Value = ((int)MeasurementUnit.tbsp).ToString(),
                Text = MeasurementUnit.tbsp.ToString()
            });

            MeasurementUnits.Add(new SelectListItem
            {
                Value = ((int)MeasurementUnit.tsp).ToString(),
                Text = MeasurementUnit.tsp.ToString()
            });

            MeasurementUnits.Add(new SelectListItem
            {
                Value = ((int)MeasurementUnit.cup).ToString(),
                Text = MeasurementUnit.cup.ToString()
            });


        }

  
            
        
    

    }
}
