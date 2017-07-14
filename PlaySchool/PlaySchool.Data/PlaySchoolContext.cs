using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using PlaySchool.Models.EntityModels;

namespace PlaySchool.Data
{
    public class PlaySchoolContext : IdentityDbContext<ApplicationUser>
    {
        public PlaySchoolContext()
            : base("PlaySchoolContext", throwIfV1Schema: false)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Player>().HasMany(x => x.Friends).WithMany()
    .Map(x => x.ToTable("Player_Friends").MapLeftKey("Player").MapRightKey("Friend"));
            modelBuilder.Entity<Player>().HasMany(x => x.PlayersRequestedFriendship).WithMany()
    .Map(x => x.ToTable("Player_PlayersRequested").MapLeftKey("Player").MapRightKey("PlayerRequested"));
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public static PlaySchoolContext Create()
        {
            return new PlaySchoolContext();
        }
    }
}