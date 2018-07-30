using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BreadBuilder.Controllers
{
    public class IngredientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            AddIngredientViewModel addIngredientViewModel = new AddIngredientViewModel();

            //creates a list from the Ingredient Data page
            List<Ingredient> ingredientList = IngredientData.GetAll();

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

                IngredientData.Add(newIngredient);

                //creates a list from the Ingredient Data page
                List<Ingredient> ingredientList = IngredientData.GetAll();

                //loops through that list and adds the items to the ingredient list in the ViewModel
                foreach(var item in ingredientList)
                {
                    addIngredientViewModel.ingredients.Add(item);
                }

                return View(addIngredientViewModel);



            }

            return View(addIngredientViewModel);

        }
    }
}