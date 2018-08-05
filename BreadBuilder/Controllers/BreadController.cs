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

        public IActionResult AddIngredient()
        {
            IList<Ingredient> ingredients = context.Ingredients.ToList();

            return View(ingredients);
        }

        /*public IActionResult AddIngredient(AddIngredientToRecipeViewModel addIngredientToRecipeViewModel)
        {
           
            
            
            return Redirect("Bread/Add");
        }*/

        public IActionResult Add()
        {
            
            
            AddBreadViewModel addBreadViewModel = new AddBreadViewModel();
            
            return View(addBreadViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddBreadViewModel addBreadViewModel)
        {
            if (ModelState.IsValid)
            {
                Bread newBread = new Bread
                {
                    Name = addBreadViewModel.Name,
                    Instructions = addBreadViewModel.Instructions
                };

                context.Breads.Add(newBread);
                context.SaveChanges();

                return Redirect("/Bread");
            }

            return View(addBreadViewModel);

        }  
    }
}