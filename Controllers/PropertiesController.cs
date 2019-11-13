using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Core;

namespace Blog.Controllers
{
    public class PropertiesController : Controller
    {
        private ITagCategory db;
        public PropertiesController(ITagCategory repository) => db = repository;

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await db.AddCategory(category);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await db.DeleteCategory(id);
            return RedirectToAction("Index", "Home");
        }
    }
}