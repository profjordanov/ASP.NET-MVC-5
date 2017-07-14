namespace PlaySchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstAndLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "FirstName", c => c.String(maxLength: 15));
            AddColumn("dbo.Players", "LastName", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "LastName");
            DropColumn("dbo.Players", "FirstName");
        }
    }
}
