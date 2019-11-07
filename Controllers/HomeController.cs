using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Blog.Core;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private IPostRepository db;
        public HomeController(IPostRepository repository) => db = repository;

        public IActionResult Index()
        {
            return View(db.Posts);
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            ViewData["Categories"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            await db.AddPost(post);
            return RedirectToAction("Index");
        }
    }
}
