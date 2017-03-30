namespace CameraBazaar.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CameraBazaar.Data.CameraBazaarContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "CameraBazaar.Data.CameraBazaarContext";
        }

        protected override void Seed(CameraBazaar.Data.CameraBazaarContext context)
        {
         
        }
    }
}
