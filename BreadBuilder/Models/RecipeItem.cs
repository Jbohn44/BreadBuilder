using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Models;

namespace BreadBuilder.Models
{
    public class RecipeItem
    {
        public int ID { get; set; }

        public Ingredient RecipeIngredient { get; set; }

        public Measurement RecipeMeasurement { get; set; }

        public IList<Bread> Breads { get; set; }

    }
}
