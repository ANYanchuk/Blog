using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public interface ITagCategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        IQueryable<Tag> Tags { get; }
        Task AddCategory(Category category);
        Task DeleteCategory(int id);
        Task AddTag(Tag tag);
        Task DeleteTag(int id);
    }
}