using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlaySchool.Models.EntityModels;

namespace PlaySchool.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PlaySchool.Data.PlaySchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "PlaySchool.Data.PlaySchoolContext";
        }

        protected override void Seed(PlaySchool.Data.PlaySchoolContext context)
        {
            //Create Admin role
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }
            //Create Player role
            if (!context.Roles.Any(r => r.Name == "Player"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Player" };

                manager.Create(role);
            }
            //Create admin
            if (!context.Users.Any(u => u.UserName == "root"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "root",Email = "changeit@example.com"};
                
                var result = manager.Create(user,"@Sd123");
                manager.AddToRole(user.Id, "Admin");
                manager.AddToRole(user.Id, "Player");
                context.Players.Add(new Player() {AppUser = context.Users.Find(user.Id),Points = 5});
                context.SaveChanges();
            }
            CreateQuestionsQuizAndGame(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private static void CreateQuestionsQuizAndGame(PlaySchoolContext context)
        {
            if (context.Games.Any())
            {
                for (int i = 0; i < 20; i++)
                {

                    Game game = new Game()
                    {
                        Description = PlaySchool.Models.Constants.SampleGameDescription,
                        JsUrl = PlaySchool.Models.Constants.JsGameUrlBBreaker,
                        Name = "BBreaker" + (i+1)
                    };
                    game.Creator = context.Users.FirstOrDefault(u => u.UserName == "root");
                    //ADD GAME
                    context.Games.Add(game);
                    Quiz quiz = new Quiz()
                    {
                        Creator = context.Users.FirstOrDefault(u => u.UserName == "root"),
                        Game = game,
                    };
                    //ADD QUIZZES
                    context.Quizzes.Add(quiz);
                    context.SaveChanges();

                    Question quest1 = new Question()
                    {
                        Answer = "4",
                        QuestionTitle = "How many row of bricks BBreaker have?",
                        QuestionTip = "Write the number you think is the answer!(ex: 4)",
                        Quiz = quiz
                    };
                    Question quest2 = new Question()
                    {
                        Answer = "Yes",
                        QuestionTitle = "The game continue after you destroy all bricks?",
                        QuestionTip = "Yes or No?(ex: Yes)",
                        Quiz = quiz
                    };
                    Question quest3 = new Question()
                    {
                        Answer = "5",
                        QuestionTitle = "How many column of bricks BBreaker have?",
                        QuestionTip = "Write the number you think is the answer!(ex. 5)",
                        Quiz = quiz
                };
                    //ADD QUESTIONS
                    context.Questions.AddRange(new List<Question>()
                    {
                        quest1,
                        quest2,
                        quest3
                    });
                }
            }
            context.SaveChanges();
        }
    }
}
