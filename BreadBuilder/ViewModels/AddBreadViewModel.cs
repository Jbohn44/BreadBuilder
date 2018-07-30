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



        public AddBreadViewModel() { }
    

    }
}
