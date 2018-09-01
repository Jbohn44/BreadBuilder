using System;
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
                //holds the list of recipe items from viewmodel
                List<RecipeItem> RecipeItemList = new List<RecipeItem>();

                //instantiates and saves a new bread recipe
                Bread newBread = new Bread
                {
                    Name = addBreadViewModel.Name,
                    Instructions = addBreadViewModel.Instructions,
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
                if (i.RecipeIngredient.Name.Contains("Flour"))
                {
                    flourValue = i.RecipeMeasurement.Value;
                }
                if (i.RecipeIngredient.Name.Contains("Water"))
                {
                    waterValue = i.RecipeMeasurement.Value;
                }
            }
            
            double hydration = Conversions.HydrationLevel(flourValue, waterValue);

            double totalWeight = Conversions.TotalWeight(items);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items,
                Hydration = hydration,
                TotalWeight = totalWeight
            };
         
            return View(viewModel);
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
                Instructions = theBread.Instructions
            };

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
                
                context.Breads.Update(theBread);
                
                //updates the recipe item, ingredient, and measurement
                //currently creating new database entries for ingredient and measurement
                //needs to update database entries instead of creating new entries
                for(var i =0; i < items.Count; i++)
                {
                    items[i].RecipeIngredient = recipeItems[i].RecipeIngredient;
                    items[i].RecipeMeasurement = recipeItems[i].RecipeMeasurement;

                    context.Ingredients.Update(items[i].RecipeIngredient);
                    context.Measurements.Update(items[i].RecipeMeasurement);
                    context.RecipeItems.Update(items[i]);
                }

                context.SaveChanges();

              



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
                context.RecipeItems.Remove(item);
            }
            context.Breads.Remove(theBread);

            context.SaveChanges();
            return RedirectToAction("UserRecipeList", "User");
        }
    }
}