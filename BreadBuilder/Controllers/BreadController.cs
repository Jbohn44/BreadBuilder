using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BreadBuilder.Controllers
{
    public class BreadController : Controller
    {
        public IActionResult Index()
        {
            List<Bread> breads = BreadData.GetAll();

            return View(breads);
        }

        public IActionResult Add()
        {
            List<Ingredient> breadIngredients = IngredientData.GetAll();
            AddBreadViewModel addBreadViewModel = new AddBreadViewModel();
            addBreadViewModel.breadIngredients = breadIngredients;
            return View(addBreadViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddBreadViewModel addBreadViewModel)
        {
            if (ModelState.IsValid)
            {
                Bread newBread = new Bread
                {
                    Name = addBreadViewModel.Name
                };

                BreadData.Add(newBread);

                return Redirect("/Bread");
            }

            return View(addBreadViewModel);

        }  
    }
}