using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    //This represents the join table for the many-to-many relationship for breads and recipeitems.
    public class BreadRecipeItem
    {
        public int RecipeItemID { get; set; }
        public RecipeItem RecipeItem { get; set; }

        public int BreadID { get; set; }
        public Bread Bread { get; set; }
    }
}
