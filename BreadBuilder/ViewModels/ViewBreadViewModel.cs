using BreadBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.ViewModels
{
    public class ViewBreadViewModel
    {
        public Bread Bread { get; set; }
        public IList<RecipeItem> Items { get; set; }
    }
}
