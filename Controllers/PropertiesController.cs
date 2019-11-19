using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Blog.Core;
using Blog.ViewModels;
using Blog.Models;

namespace Blog.Controllers
{
    public class PropertiesController : Controller
    {
        private ITagCategoryRepository db;
        public PropertiesController(ITagCategoryRepository repository) => db = repository;

        [HttpGet]
        public IActionResult Category(int id, int page = 1)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();
            ViewData["CategoryName"] = category.Name;
            return View(new IndexViewModel
            {
                Posts = category.Posts,
                PageModel = new PageModel(category.Posts.Count, page, 4, "Category", "Properties")
            });
        }

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