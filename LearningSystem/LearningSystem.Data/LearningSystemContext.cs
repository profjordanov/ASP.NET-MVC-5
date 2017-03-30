using LearningSystem.Models.EntityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class LearningSystemContext : IdentityDbContext<ApplicationUser>
    {
        
        public LearningSystemContext()
             : base(@"data source=(localdb)\MSSQLLocalDB;initial catalog=LearningSystemDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public static LearningSystemContext Create()
        {
            return new LearningSystemContext();
        }


    }

    
}