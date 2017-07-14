namespace PlaySchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quizfix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Questions", new[] { "Quiz_QuizId" });
            AlterColumn("dbo.Questions", "Quiz_QuizId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "Quiz_QuizId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Questions", new[] { "Quiz_QuizId" });
            AlterColumn("dbo.Questions", "Quiz_QuizId", c => c.Int());
            CreateIndex("dbo.Questions", "Quiz_QuizId");
        }
    }
}
