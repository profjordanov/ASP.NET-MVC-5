using CameraBazaar.Models.Enitities;

namespace CameraBazaar.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CameraBazaarContext : DbContext
    {

        public CameraBazaarContext()
            : base("name=CameraBazaar")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Camera> Cameras { get; set; }
        public virtual DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>().Property(camera => camera.Price).HasPrecision(16, 2);
            base.OnModelCreating(modelBuilder);
        }
    }


}