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

                addIngredientViewModel.ingredients.Add(newIngredient);

                return Redirect("/Ingredient/Add");



            }

            return View(addIngredientViewModel);

        }
    }
}