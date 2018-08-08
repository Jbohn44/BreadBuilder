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
    public class UserController : Controller
    {
        private BreadDbContext context;

        public UserController(BreadDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            return Redirect("Add");
        }

        public IActionResult Add()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();

            return View(addUserViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    Name = addUserViewModel.Username,
                    Password = addUserViewModel.Password
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                return RedirectToAction("UserRecipeList");
            }
            return View(addUserViewModel);
        }

        public IActionResult UserRecipeList()
        {
            List<Bread> breads = context.Breads.ToList();
            return View(breads);
        }
    }
}