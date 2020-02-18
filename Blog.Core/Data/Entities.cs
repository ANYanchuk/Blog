using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blog.Core
{
    public class Tag
    {
        public Tag() => PostTags = new List<PostTag>();

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }

        public List<PostTag> PostTags { get; set; }
    }

    public class Post
    {
        public Post()
        {
            Created = DateTime.Now;
            PostTags = new List<PostTag>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public bool Modified { get; set; } = false;
        public DateTime Created { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<PostTag> PostTags { get; set; }
    }

    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

    public class Category
    {
        public Category() => Posts = new List<Post>();
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Post> Posts { get; set; }
    }
}
