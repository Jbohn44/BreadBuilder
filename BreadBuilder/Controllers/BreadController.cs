using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreadBuilder.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreadBuilder.Controllers
{
    public class BreadController : Controller
    {
        public IActionResult Index()
        {
            IList<Bread> breads = new List<Bread>();

            return View(breads);
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}