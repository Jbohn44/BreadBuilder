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

        public List<SelectListItem> MeasurementUnits { get; set; }

        public AddBreadViewModel()
        {
            MeasurementUnits = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = ((int)Measurement.MeasurmentUnit.oz).ToString(),
                    Text = Measurement.MeasurmentUnit.oz.ToString()
                },

                new SelectListItem
                {
                    Value = ((int)Measurement.MeasurmentUnit.g).ToString(),
                    Text = Measurement.MeasurmentUnit.g.ToString()
                },

                new SelectListItem
                {
                    Value = ((int)Measurement.MeasurmentUnit.tbsp).ToString(),
                    Text = Measurement.MeasurmentUnit.tbsp.ToString()
                },

                new SelectListItem
                {
                    Value = ((int)Measurement.MeasurmentUnit.tsp).ToString(),
                    Text = Measurement.MeasurmentUnit.tsp.ToString()
                },

                new SelectListItem
                {
                    Value = ((int)Measurement.MeasurmentUnit.cup).ToString(),
                    Text = Measurement.MeasurmentUnit.cup.ToString()
                }
            };





        }
    

    }
}
