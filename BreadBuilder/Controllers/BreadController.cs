﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

namespace BreadBuilder.Controllers
{
    public class BreadController : Controller
    {
        private BreadDbContext context;
        
        public BreadController(BreadDbContext dbContext)
        {
            context = dbContext;
        }

    

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
                int userId = Convert.ToInt32(TempData["UserId"].ToString());
                //holds the list of recipe items from viewmodel
                List<RecipeItem> RecipeItemList = new List<RecipeItem>();

                //instantiates and saves a new bread recipe
                Bread newBread = new Bread
                {
                    Name = addBreadViewModel.Name,
                    Instructions = addBreadViewModel.Instructions,
                    BakeTemp = addBreadViewModel.BakeTemp,
                    BakeTime = addBreadViewModel.BakeTime,
                    UserID = userId,
                    RecipeItems = addBreadViewModel.RecipeItems.ToList()
                    
                };

                context.Breads.Add(newBread);
                context.SaveChanges();

                //loops through each recipe item in viewmodel and instantiates a new RecipeItem--Also adds said RecipeItem to a List
                foreach (var item in addBreadViewModel.RecipeItems)
                {
                    RecipeItem newRecipeItem = new RecipeItem
                    {
                        RecipeIngredient = item.RecipeIngredient,
                        RecipeMeasurement = item.RecipeMeasurement,
                        Bread = newBread
                    };
                    context.RecipeItems.Add(newRecipeItem);
                    context.SaveChanges();
                    RecipeItemList.Add(newRecipeItem);
                }


                TempData["UserId"] = userId;
                TempData.Keep();

                return RedirectToAction($"/ViewBread/{newBread.ID}");
            }

            return View(addBreadViewModel);

        }  

       public IActionResult ViewBread(int id)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == id).ToList();
            Bread theBread = context.Breads.Single(b => b.ID == id);

            double flourValue = 0;
            double waterValue = 0;
            
            foreach(var i in items)
            {
                if (KeyWordLists.Flours.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    flourValue = i.RecipeMeasurement.Value;
                }
                if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    waterValue = i.RecipeMeasurement.Value;
                }
            }
            
            double hydration = Conversions.HydrationLevel(flourValue, waterValue);

            List<double> totalWeights = Conversions.TotalWeights(items);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items,
                Hydration = hydration,
                TotalWeights = totalWeights
            };

            TempData.Keep();
            
            return View(viewModel);
        }

        public IActionResult ConvertToGrams(int id)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == id).ToList();
            Bread theBread = context.Breads.Single(b => b.ID == id);

            double flourValue = 0;
            double waterValue = 0;

            //checks to see if name of ingredient is in Lists of keywords...
            foreach (var i in items)
            {
                if (KeyWordLists.Flours.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    flourValue = i.RecipeMeasurement.Value;
                }
                if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    waterValue = i.RecipeMeasurement.Value;
                }
            }

            double hydration = Conversions.HydrationLevel(flourValue, waterValue);

            

            foreach(var i in items)
            {
                if(i.RecipeMeasurement.Unit == MeasurementUnit.oz)
                {
                    i.RecipeMeasurement.Unit = MeasurementUnit.g;
                    i.RecipeMeasurement.Value = Conversions.OuncesToGrams(i.RecipeMeasurement.Value);
                }
            }

            List<double> totalWeights = Conversions.TotalWeights(items);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items,
                Hydration = hydration,
                TotalWeights = totalWeights
            };

            TempData.Keep();

            return View("ViewBread", viewModel);
        }

        public IActionResult ConvertToOunces(int id)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == id).ToList();
            Bread theBread = context.Breads.Single(b => b.ID == id);

            double flourValue = 0;
            double waterValue = 0;

            //checks to see if name of ingredient is contained in lists of keywords
            foreach (var i in items)
            {
                if (KeyWordLists.Flours.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    flourValue = i.RecipeMeasurement.Value;
                }
                if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    waterValue = i.RecipeMeasurement.Value;
                }
            }

            double hydration = Conversions.HydrationLevel(flourValue, waterValue);

            

            foreach (var i in items)
            {
                if (i.RecipeMeasurement.Unit == MeasurementUnit.g)
                {
                    i.RecipeMeasurement.Unit = MeasurementUnit.oz;
                    i.RecipeMeasurement.Value = Conversions.GramsToOunces(i.RecipeMeasurement.Value);
                }
            }

            List<double> totalWeights = Conversions.TotalWeights(items);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items,
                Hydration = hydration,
                TotalWeights = totalWeights
            };

            TempData.Keep();

            return View("ViewBread", viewModel);
        }

        public IActionResult EditBread(int id)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == id).ToList();
            Bread theBread = context.Breads.Single(b => b.ID == id);

            EditBreadViewModel viewModel = new EditBreadViewModel
            {
                ID = theBread.ID,
                Name = theBread.Name,
                RecipeItems = items,
                Instructions = theBread.Instructions,
                BakeTime = theBread.BakeTime,
                BakeTemp = theBread.BakeTemp
            };

            TempData.Keep();

            return View(viewModel);

        }

        [HttpPost]
        public IActionResult EditBread(EditBreadViewModel editBreadViewModel)
        {
            if (ModelState.IsValid)
            {
                //holds the list of recipe items from viewmodel
                List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == editBreadViewModel.ID).ToList();
                Bread theBread = context.Breads.Single(b => b.ID == editBreadViewModel.ID);
                List<RecipeItem> recipeItems = editBreadViewModel.RecipeItems.ToList();
                theBread.Name = editBreadViewModel.Name;
                theBread.Instructions = editBreadViewModel.Instructions;
                theBread.BakeTemp = editBreadViewModel.BakeTemp;
                theBread.BakeTime = editBreadViewModel.BakeTime;
                context.Breads.Update(theBread);
                
                //updates the recipe item, ingredient, and measurement
                //currently creating new database entries for ingredient and measurement
                //needs to update database entries instead of creating new entries
                for(var i =0; i < items.Count; i++)
                {
                    items[i].RecipeIngredient.Name = recipeItems[i].RecipeIngredient.Name;
                    items[i].RecipeMeasurement.Value = recipeItems[i].RecipeMeasurement.Value;
                    items[i].RecipeMeasurement.Unit = recipeItems[i].RecipeMeasurement.Unit;

                  
                    context.RecipeItems.Update(items[i]);
                }

                context.SaveChanges();



                TempData.Keep();

                return RedirectToAction($"/ViewBread/{editBreadViewModel.ID}");
            }

            return View(editBreadViewModel);



        }

     


        public IActionResult Delete(int id)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == id).ToList();
            Bread theBread = context.Breads.Single(b => b.ID == id);

            foreach(var item in items)
            {
                context.Ingredients.Remove(item.RecipeIngredient);
                context.Measurements.Remove(item.RecipeMeasurement);
                context.RecipeItems.Remove(item);
            }
            context.Breads.Remove(theBread);

            context.SaveChanges();
            return RedirectToAction("UserRecipeList", "User");
        }
    }
}