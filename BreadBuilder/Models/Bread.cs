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

        public IList<Ingredient> ingredients { get; set; }

    }
}
