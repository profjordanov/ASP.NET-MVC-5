using System.Data.Entity;
using System.Data.Entity.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(typeof(ODataServer.App_Start.EntityFramework_SqlServerCompact), "Start")]

namespace ODataServer.App_Start {
    public static class EntityFramework_SqlServerCompact {
        public static void Start() {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Database.SetInitializer<ODataServer.Models.TaskDbContext>(
                new System.Data.Entity.CreateDatabaseIfNotExists<ODataServer.Models.TaskDbContext>());
        }
    }
}
