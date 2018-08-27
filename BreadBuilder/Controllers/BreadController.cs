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

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items
            };
         
            return View(viewModel);
        }

        public IActionResult EditBread(int id)
        {
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == id).ToList();
            Bread theBread = context.Breads.Single(b => b.ID == id);

            EditBreadViewModel viewModel = new EditBreadViewModel
            {
                Name = theBread.Name,
                RecipeItems = items,
                Instructions = theBread.Instructions
            };

            return View(viewModel);

        }

        //TODO  Make an HTTPPOST for EditBread handler

        
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