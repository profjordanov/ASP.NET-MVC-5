namespace StreamPowered.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class StreamPoweredDbContext : IdentityDbContext<User>
    {
        public StreamPoweredDbContext()
            : base("StreamPoweredConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public static StreamPoweredDbContext Create()
        {
            return new StreamPoweredDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Ratings)
                .WithRequired(r => r.Game)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Reviews)
                .WithRequired(r => r.Game)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
