using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Blog.Core;
using Blog.Models;
using Blog.ViewModels;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private IPostRepository db;
        public HomeController(IPostRepository repository) => db = repository;

        public IActionResult Index(int page = 1)
        {
            const int pageSize = 4;
            return View(new IndexViewModel
            {
                Posts = db.Posts.
                    OrderByDescending(p => p.Created).
                    Skip((page - 1) * pageSize).
                    Take(pageSize),
                PageModel = new PageModel(db.Posts.Count(), page, pageSize, "Index", "Home")
            });
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            ViewData["Categories"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(Post post, string tags)
        {
            await db.AddPost(post, tags);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NotFound();
            ViewData["Tags"] = "" + db.GetPostTags(id).Select(t => t.Name).DefaultIfEmpty()?.Aggregate((s1, s2) => s1 + ' ' + s2);
            ViewData["Categories"] = new SelectList(db.Categories, "Id", "Name");
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(Post post, string tags)
        {
            await db.EditPost(post, tags);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            await db.DeletePost(id);
            return RedirectToAction("Index");
        }
    }
}
