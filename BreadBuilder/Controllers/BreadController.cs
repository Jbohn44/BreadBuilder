using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

/*
 * TODO: refactor code so that the whole conversion process for bread hydration level takes place 
 *       in the Conversions class.  Currently, it is used in several handlers--ViewBread, ConvertToGrams ect...
 *   
 * TODO: refactor code so that method calls replace the code inside of controllers... for the most part...
 */
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

                //converts the user id to an integer from tempdata
                //the user id is boxed in TempData when the user logs in
                int userId = Convert.ToInt32(TempData["UserId"].ToString());

                Bread newBread = DataBaseAccess.AddToDataBase(addBreadViewModel, userId, context);

                TempData["UserId"] = userId;

                TempData.Keep();

                return RedirectToAction($"/ViewBread/{newBread.ID}");
            }

            return View(addBreadViewModel);

        }  

       public IActionResult ViewBread(int id)
        {
            
            ViewBreadViewModel viewModel = DataBaseAccess.ViewBread(id, context);
            TempData.Keep();
            
            return View(viewModel);
        }

        //Controller converts all values of recipe items to grams...  Nothing is saved, everything is just converted for the view...
        public IActionResult ConvertToGrams(int id)
        {

            ViewBreadViewModel viewModel = DataBaseAccess.ConvertToGramsViewModel(id, context);

            TempData.Keep();

            return View("ViewBread", viewModel);
        }

        //Controller converts all values of recipe items to ounces...  Nothing is saved, everything is just converted for the view...
        public IActionResult ConvertToOunces(int id)
        {
            ViewBreadViewModel viewModel = DataBaseAccess.ConvertToOuncesViewModel(id, context);

            TempData.Keep();

            return View("ViewBread", viewModel);
        }

        //Controller for the initial view of the EditBread view...
        public IActionResult EditBread(int id)
        {

            EditBreadViewModel viewModel = DataBaseAccess.EditBreadView(id, context);

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

                theBread.FermentTime = editBreadViewModel.FermentTime;

                theBread.ProofTime = editBreadViewModel.ProofTime;

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
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient)
                .Include(y => y.RecipeMeasurement)
                .Where(x => x.Bread.ID == id)
                .ToList();

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