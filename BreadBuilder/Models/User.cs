using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }

        public List<Bread> Recipes = new List<Bread>();

    }
}

