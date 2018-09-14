using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.ViewModels
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public string Temp { get; set; }
        public string Humidity { get; set; }
        public int BreadID { get; set; }
    }
}
