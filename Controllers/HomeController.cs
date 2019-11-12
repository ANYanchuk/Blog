using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Blog.Core;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        const int pageSize = 4;
        private IPostRepository db;
        public HomeController(IPostRepository repository) => db = repository;

        public IActionResult Index(int page = 1)
        {
            IEnumerable<Post> posts = db.Posts.
                OrderByDescending(p => p.Created).
                Skip((page - 1) * pageSize).
                Take(pageSize);
            return View(posts);
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

        [HttpGet]
        public IActionResult EditPost(int id)
        {
            ViewData["Categories"] = new SelectList(db.Categories, "Id", "Name");
            return View(db.Posts.First(p=>p.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(Post post)
        {
            await db.EditPost(post);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            await db.DeletePost(id);
            return RedirectToAction("Index");
        }
    }
}
