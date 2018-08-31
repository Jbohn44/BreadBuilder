using BreadBuilder.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.ViewModels
{
    public class EditBreadViewModel
    {
        [Required]
        [Display(Name = "Bread Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }

        public int ID { get; set; }

        public IList<RecipeItem> RecipeItems { get; set; }

        public List<SelectListItem> MeasurementUnits { get; set; }

        public EditBreadViewModel()
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
                Value = ((int)MeasurementUnit.cup).ToString(),
                Text = MeasurementUnit.cup.ToString()
            });


        }
    }
}
