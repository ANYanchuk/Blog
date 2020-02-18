using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Blog.Core
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        IEnumerable<Category> Categories { get; }
        IQueryable<Tag> Tags { get; }
        Task AddPost(Post post, string tags);
        Task EditPost(Post post, string tags);
        Task DeletePost(int id);
        List<Tag> GetPostTags(int id);
    }
}
