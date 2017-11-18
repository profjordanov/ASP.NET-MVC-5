namespace Snippy.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;
    using UnitOfWork;

    public class SnippyDbContext : IdentityDbContext<User>
    {
        public SnippyDbContext()
            : base("SnippyDatabaseConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Snippet> Snippets { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Label> Labels { get; set; }

        public static SnippyDbContext Create()
        {
            return new SnippyDbContext();
        }
    }
}
