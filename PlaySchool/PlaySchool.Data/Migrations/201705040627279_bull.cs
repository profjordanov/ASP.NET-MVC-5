namespace PlaySchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bull : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Games", name: "Creator_Id", newName: "CreatorId");
            RenameIndex(table: "dbo.Games", name: "IX_Creator_Id", newName: "IX_CreatorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Games", name: "IX_CreatorId", newName: "IX_Creator_Id");
            RenameColumn(table: "dbo.Games", name: "CreatorId", newName: "Creator_Id");
        }
    }
}
