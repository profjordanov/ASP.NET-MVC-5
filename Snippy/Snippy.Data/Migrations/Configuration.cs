namespace Snippy.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System;

    public sealed class Configuration : DbMigrationsConfiguration<SnippyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SnippyDbContext context)
        {
            SeedRolesAndUsers(context);
            SeedLanguages(context);
            SeedLabels(context);
            SeedSnippets(context);
            SeedComments(context);
        }

        private static void SeedRolesAndUsers(SnippyDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Admin");

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User() { UserName = "admin", Email = "admin@snippy.softuni.com" };

                manager.Create(user, "adminPass123");
                manager.AddToRole(user.Id, "Admin");

                user = new User { UserName = "someUser", Email = "someUser@example.com" };
                manager.Create(user, "someUserPass123");

                user = new User() { UserName = "newUser", Email = "new_user@gmail.com" };
                manager.Create(user, "userPass123");
            }
        }

        private static void SeedLanguages(SnippyDbContext context)
        {
            context.Languages.AddOrUpdate(
                l => l.Id,
                new Language() { Id = 1, Name = "C#" },
                new Language() { Id = 2, Name = "JavaScript" },
                new Language() { Id = 3, Name = "Python" },
                new Language() { Id = 4, Name = "CSS" },
                new Language() { Id = 5, Name = "SQL" },
                new Language() { Id = 6, Name = "PHP" });
            context.SaveChanges();
        }

        private static void SeedLabels(SnippyDbContext context)
        {
            context.Labels.AddOrUpdate(
                l => l.Id,
                new Label() { Id = 1, Text = "bug" },
                new Label() { Id = 2, Text = "funny" },
                new Label() { Id = 3, Text = "jquery" },
                new Label() { Id = 4, Text = "mysql" },
                new Label() { Id = 5, Text = "useful" },
                new Label() { Id = 6, Text = "web" },
                new Label() { Id = 7, Text = "geometry" },
                new Label() { Id = 8, Text = "back-end" },
                new Label() { Id = 9, Text = "front-end" },
                new Label() { Id = 10, Text = "games" });
            context.SaveChanges();
        }

        private static void SeedSnippets(SnippyDbContext context)
        {
            var bug = context.Labels.FirstOrDefault(label => label.Text == "bug");
            var funny = context.Labels.FirstOrDefault(label => label.Text == "funny");
            var jquery = context.Labels.FirstOrDefault(label => label.Text == "jquery");
            var mysql = context.Labels.FirstOrDefault(label => label.Text == "mysql");
            var useful = context.Labels.FirstOrDefault(label => label.Text == "useful");
            var web = context.Labels.FirstOrDefault(label => label.Text == "web");
            var geometry = context.Labels.FirstOrDefault(label => label.Text == "geometry");
            var backEnd = context.Labels.FirstOrDefault(label => label.Text == "back-end");
            var frontEnd = context.Labels.FirstOrDefault(label => label.Text == "front-end");
            var games = context.Labels.FirstOrDefault(label => label.Text == "games");

            var admin = context.Users.FirstOrDefault(u => u.UserName == "admin");
            var newUser = context.Users.FirstOrDefault(u => u.UserName == "newUser");
            var someUser = context.Users.FirstOrDefault(u => u.UserName == "someUser");

            context.Snippets.AddOrUpdate(
                s => s.Id,
                new Snippet()
                {
                    Id = 1,
                    Title = "Ternary Operator Madness",
                    Description = "This is how you DO NOT user ternary operators in C#!",
                    Code = "bool X = Glob.UserIsAdmin ? true : false;",
                    Author = admin,
                    CreationTime = new DateTime(2015, 10, 26, 17, 20, 33),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "C#"),
                    Labels = new[] { funny }
                },
                new Snippet()
                {
                    Id = 2,
                    Title = "Points Around A Circle For GameObject Placement",
                    Description = "Determine points around a circle which can be used to place elements around a central point",
                    Code =
                    @"private Vector3 DrawCircle(float centerX, float centerY, float radius, float totalPoints, float currentPoint)
{
	float ptRatio = currentPoint / totalPoints;
	float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius;
	float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius;

	Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f);

	return panelCenter;
}",
                    Author = admin,
                    CreationTime = new DateTime(2015, 10, 26, 20, 15, 30),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "C#"),
                    Labels = new[] { geometry, games }
                },
                new Snippet()
                {
                    Id = 3,
                    Title = "forEach. How to break?",
                    Description = "Array.prototype.forEach You can't break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                    Code =
                    @"var ary = [""JavaScript"", ""Java"", ""CoffeeScript"", ""TypeScript""];
 
ary.some(function (value, index, _ary) {
	console.log(index + "": "" + value);
	return value === ""CoffeeScript"";
});
// output: 
// 0: JavaScript
// 1: Java
// 2: CoffeeScript
 
ary.every(function(value, index, _ary) {
	console.log(index + "": "" + value);
	return value.indexOf(""Script"") > -1;
});
// output:
// 0: JavaScript
// 1: Java",
                    Author = newUser,
                    CreationTime = new DateTime(2015, 10, 27, 10, 53, 20),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "JavaScript"),
                    Labels = new[] { web, jquery, frontEnd, useful }
                },
                new Snippet()
                {
                    Id = 4,
                    Title = "Numbers only in an input field",
                    Description = "Method allowing only numbers (positive / negative / with commas or decimal points) in a field",
                    Code =
                    @"$(""#input"").keypress(function(event){
	var charCode = (event.which) ? event.which : window.event.keyCode;
	if (charCode <= 13) { return true; } 
	else {
		var keyChar = String.fromCharCode(charCode);
		var regex = new RegExp(""[0-9,.-]"");
		return regex.test(keyChar); 
	} 
});",
                    Author = someUser,
                    CreationTime = new DateTime(2015, 10, 28, 9, 00, 56),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "JavaScript"),
                    Labels = new[] { frontEnd, web }
                },
                new Snippet()
                {
                    Id = 5,
                    Title = "Create a link directly in an SQL query",
                    Description = "That's how you create links - directly in the SQL!",
                    Code =
                    @"SELECT DISTINCT
              b.Id,
              concat('<button type=""""button"""" onclick=""""DeleteContact(', cast(b.Id as char), ')"""">Delete...</button>') as lnkDelete
FROM tblContact   b
WHERE ....",
                    Author = admin,
                    CreationTime = new DateTime(2015, 10, 30, 11, 20, 00),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "SQL"),
                    Labels = new[] { funny, mysql, bug }
                },
                new Snippet()
                {
                    Id = 6,
                    Title = "Reverse a String",
                    Description = "Almost not worth having a function for...",
                    Code =
                    @"def reverseString(s):
	""""""Reverses a string given to it.""""""
	return s[::-1]",
                    Author = admin,
                    CreationTime = new DateTime(2015, 10, 26, 9, 35, 13),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "Python"),
                    Labels = new[] { useful }
                },
                new Snippet()
                {
                    Id = 7,
                    Title = "Pure CSS Text Gradients",
                    Description = "This code describes how to create text gradients using only pure CSS",
                    Code =
                    @"/* CSS text gradients */
h2[data-text] {
	position: relative;
}
h2[data-text]::after {
	content: attr(data-text);
	z-index: 10;
	color: #e3e3e3;
	position: absolute;
	top: 0;
	left: 0;
	-webkit-mask-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0,0,0,0)), color-stop(50%, rgba(0,0,0,1)), to(rgba(0,0,0,0)));",
                    Author = someUser,
                    CreationTime = new DateTime(2015, 10, 22, 19, 26, 42),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "CSS"),
                    Labels = new[] { web, frontEnd }
                },
                new Snippet()
                {
                    Id = 8,
                    Title = "Check for a Boolean value in JS",
                    Description = "How to check a Boolean value - the wrong but funny way :D",
                    Code = @"var b = true;

if (b.toString().length < 5) {
  //...
}",
                    Author = admin,
                    CreationTime = new DateTime(2015, 10, 22, 5, 30, 4),
                    Language = context.Languages.FirstOrDefault(language => language.Name == "JavaScript"),
                    Labels = new[] { funny }
                });
            context.SaveChanges();
        }

        private static void SeedComments(SnippyDbContext context)
        {
            var admin = context.Users.FirstOrDefault(u => u.UserName == "admin");
            var newUser = context.Users.FirstOrDefault(u => u.UserName == "newUser");
            var someUser = context.Users.FirstOrDefault(u => u.UserName == "someUser");

            context.Comments.AddOrUpdate(
                c => c.Id,
                new Comment()
                {
                    Id = 1,
                    Author = admin,
                    CreationTime = new DateTime(2015, 10, 30, 11, 50, 38),
                    Content = "Now that's really funny! I like it!",
                    Snippet = context.Snippets.Find(1)
                },
                new Comment()
                {
                    Id = 2,
                    Author = newUser,
                    CreationTime = new DateTime(2015, 11, 1, 15, 52, 42),
                    Content = "Here, have my comment!",
                    Snippet = context.Snippets.Find(1)
                },
                new Comment()
                {
                    Id = 3,
                    Author = someUser,
                    CreationTime = new DateTime(2015, 11, 2, 5, 30, 20),
                    Content = "I didn't manage to comment first :(",
                    Snippet = context.Snippets.Find(1)
                },
                new Comment()
                {
                    Id = 4,
                    Author = newUser,
                    CreationTime = new DateTime(2015, 10, 27, 15, 28, 14),
                    Content = "That's why I love Python - everything is so simple!",
                    Snippet = context.Snippets.Find(6)
                },
                new Comment()
                {
                    Id = 5,
                    Author = someUser,
                    CreationTime = new DateTime(2015, 10, 29, 15, 8, 42),
                    Content = "I have always had problems with Geometry in school. Thanks to you I can now do this without ever having to learn this damn subject",
                    Snippet = context.Snippets.Find(2)
                },
                new Comment()
                {
                    Id = 6,
                    Author = admin,
                    CreationTime = new DateTime(2015, 11, 3, 12, 56, 20),
                    Content = "Thank you. However, I think there must be a simpler way to do this. I can't figure it out now, but I'll post it when I'm ready.",
                    Snippet = context.Snippets.Find(4)
                });
            context.SaveChanges();
        }
    }
}
