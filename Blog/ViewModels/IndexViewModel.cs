using System.Collections.Generic;

using Blog.Core;
using Blog.Models;

namespace Blog.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public PageModel PageModel { get; set; }
    }
}