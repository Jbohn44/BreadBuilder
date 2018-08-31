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

            IList<Ingredient> ingredients = context.Ingredients.ToList();
            IList<Measurement> measurements = context.Measurements.ToList();
            IList<RecipeItem> recipeItems = context.RecipeItems.ToList();

            RemoveIngredientViewModel removeIngredientViewModel = new RemoveIngredientViewModel
            {
                Ingredients = ingredients,
                Measurements = measurements,
                RecipeItems = recipeItems
            };
            return View(removeIngredientViewModel);
        }

        public IActionResult DeleteMeasurement(int id)
        {
            Measurement theMeasurement = context.Measurements.Single(m => m.ID == id);
            context.Measurements.Remove(theMeasurement);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteIngredient(int id)
        {
            Ingredient theIngredient = context.Ingredients.Single(i => i.ID == id);
            context.Ingredients.Remove(theIngredient);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteRecipeItem(int id)
        {
            RecipeItem theRecipeItem = context.RecipeItems.Single(r => r.ID == id);
            context.RecipeItems.Remove(theRecipeItem);
            context.SaveChanges();

            return RedirectToAction("Index");
        }


        /*
         * Section For adding individual ingredients...  Needs to be deleted if not used...
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
        */
    }
}