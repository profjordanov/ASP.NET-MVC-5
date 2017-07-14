namespace PlaySchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class games : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        NumberPlayed = c.Int(nullable: false),
                        JsUrl = c.String(nullable: false),
                        Creator_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.Quizzes",
                c => new
                    {
                        QuizId = c.Int(nullable: false, identity: true),
                        Creator_Id = c.String(nullable: false, maxLength: 128),
                        Game_GameId = c.Int(),
                    })
                .PrimaryKey(t => t.QuizId)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .ForeignKey("dbo.Games", t => t.Game_GameId)
                .Index(t => t.Creator_Id)
                .Index(t => t.Game_GameId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionTitle = c.String(nullable: false),
                        QuestionTip = c.String(),
                        Answer = c.String(nullable: false),
                        Quiz_QuizId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Quizzes", t => t.Quiz_QuizId)
                .Index(t => t.Quiz_QuizId);
            
            CreateTable(
                "dbo.QuizPlayers",
                c => new
                    {
                        Quiz_QuizId = c.Int(nullable: false),
                        Player_PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quiz_QuizId, t.Player_PlayerId })
                .ForeignKey("dbo.Quizzes", t => t.Quiz_QuizId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId, cascadeDelete: true)
                .Index(t => t.Quiz_QuizId)
                .Index(t => t.Player_PlayerId);
            
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "Quiz_QuizId", "dbo.Quizzes");
            DropForeignKey("dbo.QuizPlayers", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.QuizPlayers", "Quiz_QuizId", "dbo.Quizzes");
            DropForeignKey("dbo.Quizzes", "Game_GameId", "dbo.Games");
            DropForeignKey("dbo.Quizzes", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.QuizPlayers", new[] { "Player_PlayerId" });
            DropIndex("dbo.QuizPlayers", new[] { "Quiz_QuizId" });
            DropIndex("dbo.Questions", new[] { "Quiz_QuizId" });
            DropIndex("dbo.Quizzes", new[] { "Game_GameId" });
            DropIndex("dbo.Quizzes", new[] { "Creator_Id" });
            DropIndex("dbo.Games", new[] { "Creator_Id" });
            DropTable("dbo.QuizPlayers");
            DropTable("dbo.Questions");
            DropTable("dbo.Quizzes");
            DropTable("dbo.Games");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
