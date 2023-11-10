using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    #pragma warning disable CS1591
    public class BlogDataContext : DbContext
    {

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$");
        // }

        public BlogDataContext(DbContextOptions<BlogDataContext>options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Post> Posts{ get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

    }
}
    #pragma warning disable CS1591  