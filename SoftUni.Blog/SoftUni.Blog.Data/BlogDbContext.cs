namespace SoftUni.Blog.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SoftUni.Blog.Models;
    using System.Data.Entity;

    public class BlogDbContext : IdentityDbContext<User>
    {
        public BlogDbContext()
            : base("SoftUniBlogConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
    }
}
