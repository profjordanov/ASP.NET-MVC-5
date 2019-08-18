using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ODataServer.Models
{
    public class TaskDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<ODataServer.Models.TaskDbContext>());

        public TaskDbContext()
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<ODataServer.Models.Task> Tasks { get; set; }

        public DbSet<ODataServer.Models.User> Users { get; set; }

        public DbSet<ODataServer.Models.TaskPriority> Priorities { get; set; }

        public DbSet<ODataServer.Models.TaskStatus> Statuses { get; set; }
    }
}