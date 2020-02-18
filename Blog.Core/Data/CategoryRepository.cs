using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core
{
    public class CategoryRepository : ITagCategoryRepository
    {
        private PostContext db;

        public CategoryRepository(PostContext ctx) => db = ctx;

        public IEnumerable<Category> Categories => db.Categories.Include(c => c.Posts);
        public IQueryable<Tag> Tags => db.Tags;

        public async Task AddCategory(Category category)
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
        }

        public async Task AddTag(Tag tag)
        {
            await db.Tags.AddAsync(tag);
            await db.SaveChangesAsync();
        }

        public async Task DeleteTag(int id)
        {
            Tag tag = await db.Tags.FindAsync(id);
            db.Tags.Remove(tag);
            await db.SaveChangesAsync();
        }
    }
}
