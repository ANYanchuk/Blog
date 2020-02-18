using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public class PostRepository : IPostRepository
    {
        private PostContext db;
        public PostRepository(PostContext ctx) => db = ctx;

        public IQueryable<Post> Posts => db.Posts.Include(p => p.Category).
            Include(p => p.PostTags).ThenInclude(pt => pt.Tag);
        public IEnumerable<Category> Categories => db.Categories;
        public IQueryable<Tag> Tags => db.Tags;

        public async Task AddPost(Post post, string tags)
        {
            post.PostTags = CreatePostTags(post, tags);
            await db.AddAsync(post);
            await db.SaveChangesAsync();
        }

        // Note: To change data in many-to-many relation, you should use ontly DbSet
        public async Task EditPost(Post post, string tags)
        {
            var postTags = CreatePostTags(post, tags);
            var postTagSet = db.Set<PostTag>();
            
            postTagSet.RemoveRange(postTagSet.Where(pt => pt.PostId == post.Id));
            await db.Tags.ForEachAsync(t => t.Rating--);

            // Deletes tags with zero rating
            db.Tags.RemoveRange(db.Tags.Where(t => t.Rating == 0));

            postTagSet.UpdateRange(postTags);
            post.Modified = true;
            db.Posts.Update(post);
            await db.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = await db.Posts.FindAsync(id);
            await db.Tags.ForEachAsync(t => t.Rating--);

            // Deletes tags with zero rating
            db.Tags.RemoveRange(db.Tags.Where(t => t.Rating == 0));

            db.Posts.Remove(post);
            await db.SaveChangesAsync();
        }

        public List<Tag> GetPostTags(int id)
        {
            var post = db.Posts.Find(id);
            return new List<Tag>(post.PostTags.Select(pt => pt.Tag));
        }

        // Parses string to list of tags and create relation between posts and tags
        private List<PostTag> CreatePostTags(Post post, string tags)
        {
            //Parses string to tag list
            List<Tag> tagList = tags.Split(new char[] { ' ', '\n' }).Distinct().ToList().
                Select(s =>
                    {
                        Tag tag = db.Tags.FirstOrDefault(t => t.Name == s);
                        tag ??= db.Tags.Add(new Tag { Name = s, Rating = 0 }).Entity;
                        return tag;
                    }).ToList();

            var postTags = new List<PostTag>();
            foreach (Tag t in tagList)
            {
                t.Rating++;
                postTags.Add(new PostTag { Post = post, Tag = t });
            }
            return postTags;
        }
    }
}
