﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Models;

namespace BreadBuilder.Models
{
    public class RecipeItem
    {
        public Ingredient RecipeIngredient { get; set; }

        public Measurement RecipeMeasurement { get; set; }

    }
}
