using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BreadBuilder.Controllers
{
    public class IngredientController : Controller
    {
        private BreadDbContext context;

        public IngredientController(BreadDbContext dbContext)
        {
            context = dbContext;
        }


        public IActionResult Index()
        {

            IList<Ingredient> viewIngredients = context.Ingredients.ToList();
            return View(viewIngredients);
        }

        public IActionResult Add()
        {
            AddIngredientViewModel addIngredientViewModel = new AddIngredientViewModel();

            //creates a list from the Ingredient Data page
            List<Ingredient> ingredientList = context.Ingredients.ToList();

            //loops through that list and adds the items to the ingredient list in the ViewModel
            foreach (var item in ingredientList)
            {
                addIngredientViewModel.ingredients.Add(item);
            }


            return View(addIngredientViewModel);
        }
        
        

        [HttpPost]
        public IActionResult Add(AddIngredientViewModel addIngredientViewModel)
        {
            if (ModelState.IsValid)
            {
                Ingredient newIngredient = new Ingredient
                {
                    Name = addIngredientViewModel.Name

                };

                context.Ingredients.Add(newIngredient);
                context.SaveChanges();

                //creates a list from the Ingredient Data page
                List<Ingredient> ingredientList = context.Ingredients.ToList();

                //loops through that list and adds the items to the ingredient list in the ViewModel
                foreach(var item in ingredientList)
                {
                    addIngredientViewModel.ingredients.Add(item);
                }

                

                return Redirect("/Ingredient");



            }

            return View(addIngredientViewModel);

        }
    }
}