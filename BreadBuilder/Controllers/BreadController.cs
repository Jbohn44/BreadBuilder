﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreadBuilder.Controllers
{
    public class BreadController : Controller
    {
        private BreadDbContext context;
        
        public BreadController(BreadDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            //should return a list of breads... May need to be revised
            IList<Bread> breads = context.Breads.ToList();

            return View(breads);
        }

        public IActionResult Add()
        {
            //passes a list of already added ingredients to the bread creation page
            
            AddBreadViewModel addBreadViewModel = new AddBreadViewModel();
            
            return View(addBreadViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddBreadViewModel addBreadViewModel)
        {
            if (ModelState.IsValid)
            {
                /*Ingredient newIngredient = new Ingredient
                {
                    Name = addBreadViewModel.Ingredient.Name
                };
                context.Ingredients.Add(newIngredient);
                context.SaveChanges();
                Measurement newMeasurement = new Measurement
                {
                    Value = addBreadViewModel.Measurement.Value,
                    Unit = addBreadViewModel.Measurement.Unit
                };
                context.Measurements.Add(newMeasurement);
                context.SaveChanges();*/

               RecipeItem newRecipeItem = new RecipeItem
                {
                    RecipeIngredient = addBreadViewModel.Ingredient,
                    RecipeMeasurement = addBreadViewModel.Measurement
                };
                context.RecipeItems.Add(newRecipeItem);
                context.SaveChanges();
                Bread newBread = new Bread
                {
                    Name = addBreadViewModel.Name,
                    Instructions = addBreadViewModel.Instructions
                    
                };

                context.Breads.Add(newBread);
                context.SaveChanges();

                BreadRecipeItem breadRecipeItem = new BreadRecipeItem
                {
                    Bread = newBread,
                    RecipeItem = newRecipeItem
                };
                context.BreadRecipeItems.Add(breadRecipeItem);
                context.SaveChanges();

                return Redirect("/Bread");
            }

            return View(addBreadViewModel);

        }  
    }
}