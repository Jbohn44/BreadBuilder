using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public class Bread
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }

        public int RecipeItemID { get; set; }
        //public IList<RecipeItem> BreadIngredients{ get; set; }


       

    }
}
