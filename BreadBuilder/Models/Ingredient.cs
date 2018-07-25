using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int nextId = 1;

        public Ingredient()
        {
            ID = nextId;

            nextId++;
        }
    }
}
