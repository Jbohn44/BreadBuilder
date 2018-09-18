using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;

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
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                IList<User> existingUsers = context.Users.ToList();
                IList<String> existingUserNames = new List<String>();


                //loop through existing users and adds name to list...
                foreach(var user in existingUsers)
                {
                    existingUserNames.Add(user.Name);
                }
                if (existingUserNames.Contains(addUserViewModel.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(addUserViewModel);
                }
                else
                {


                    User newUser = new User
                    {
                        Name = addUserViewModel.Username,
                        Password = addUserViewModel.Password
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    TempData["UserID"] = newUser.ID;
                    TempData.Keep();

                    return RedirectToAction($"UserRecipeList/{newUser.ID}");

                }
            }
            return View(addUserViewModel);
        }

        
        public IActionResult Login()
        {

            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                User user = context.Users.Single(u => u.Name == loginViewModel.Username);
                if (user.Password == loginViewModel.Password)
                {

                    TempData["UserId"] = user.ID;
                    TempData.Keep();

                    return RedirectToAction($"UserRecipeList/{user.ID}", "User");
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid Password or Username");
                    return View(loginViewModel);
                }
            }
            return View(loginViewModel);
        }

        public IActionResult UserRecipeList()
        {
            
            if (TempData["UserID"] != null)
            {
                int id = (int)TempData["UserId"];
                List<Bread> breads = context.Breads.Where(b => b.UserID == id).ToList();

                TempData.Keep();

                return View(breads);

                
            }
            else
            {
                return RedirectToAction("Add", "User");
            }
        }

        public IActionResult Logout()
        {
            TempData.Clear();

            return RedirectToAction("Login");
        }
    }
}