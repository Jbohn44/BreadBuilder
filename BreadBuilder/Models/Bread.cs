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
        public double BakeTime { get; set; }
        public int BakeTemp { get; set; }
        public int UserID { get; set; }

       


        public List<RecipeItem> RecipeItems = new List<RecipeItem>();


       

    }
}
