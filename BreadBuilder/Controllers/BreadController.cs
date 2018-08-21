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

                //loops through each recipe item in viewmodel and instantiates a new RecipeItem--Also adds said RecipeItem to a List
               foreach(var item in addBreadViewModel.RecipeItems)
                {
                    RecipeItem newRecipeItem = new RecipeItem
                    {
                        RecipeIngredient = item.RecipeIngredient,
                        RecipeMeasurement = item.RecipeMeasurement
                    };
                    context.RecipeItems.Add(newRecipeItem);
                    context.SaveChanges();
                    RecipeItemList.Add(newRecipeItem);
                } 
               
                //instantiates and saves the Bread
                Bread newBread = new Bread
                {
                    Name = addBreadViewModel.Name,
                    Instructions = addBreadViewModel.Instructions
                    
                };

                context.Breads.Add(newBread);
                context.SaveChanges();

                var breadId = newBread.ID;  //to delete... maybe not needed

                //cycles through the list of recipe items then adds them to database with bread
                foreach(var item in RecipeItemList)
                {
                    BreadRecipeItem newBreadRecipeItem = new BreadRecipeItem
                    {
                        Bread = newBread,
                        RecipeItem = item
                    };
                    context.BreadRecipeItems.Add(newBreadRecipeItem);
                    context.SaveChanges();
                }

                return Redirect("/Bread");
            }

            return View(addBreadViewModel);

        }  

        public IActionResult ViewBread(int id)
        {
            List<BreadRecipeItem> items = context
                .BreadRecipeItems
                .Include(item => item.RecipeItem)
                .Where(bri => bri.BreadID == id)
                .ToList();
            Bread theBread = context.Breads.Single(b => b.ID == id);

            ViewBreadViewModel viewModel = new ViewBreadViewModel
            {
                Bread = theBread,
                Items = items
            };
         
            return View(viewModel);
        }
    }
}