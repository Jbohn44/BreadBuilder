using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    //class that contains Measurement Value and Measurement unit for RecipeItem.  
    public class Measurement
    {
        public int ID { get; set; }
        public int MeasurementValue { get; set; }

        public enum MeasurmentUnit
        {
            oz, g, tbsp, tsp, cup
        }
    }
}
