using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Blog.Core
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BlogDB;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var pt = modelBuilder.Entity<PostTag>();

            pt.HasKey(t => new { t.PostId, t.TagId });
            pt.HasOne(pt => pt.Post).
              WithMany(p => p.PostTags).
              HasForeignKey(pt => pt.PostId);

            pt.HasOne(pt => pt.Tag).
              WithMany(t => t.PostTags).
              HasForeignKey(pt => pt.TagId);
        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostContext>
    {
        public PostContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BlogDB;");
            return new PostContext(optionsBuilder.Options);
        }
    }
}
