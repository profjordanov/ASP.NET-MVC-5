namespace SportSystem.Data.Migrations
{
    using System;

    using SportSystem.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SportSystem.Data.SportSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SportSystemDbContext context)
        {
            if (!context.Teams.Any())
            {
                var teams = this.LoadTeams(context);
                this.LoadMatches(context, teams);
                this.LoadPlayers(context, teams);
                this.LoadComments(context);
                this.LoadUserMatchBets(context);
            }

            base.Seed(context);
        }

        private List<Team> LoadTeams(SportSystemDbContext context)
        {
            var teams = new List<Team>()
            {
                new Team() { Name = "Manchester United F.C.", Website = "http://www.manutd.com", Founded = new DateTime(1878, 1, 1), Nickname = "The Red Devils" },
                new Team() { Name = "Real Madrid", Website = "http://www.realmadrid.com", Founded = new DateTime(1902, 3, 6), Nickname = "The Whites" },
                new Team() { Name = "FC Barcelona", Website = "http://www.fcbarcelona.com", Founded = new DateTime(1899, 11, 12), Nickname = "Barca" },
                new Team() { Name = "Bayern Munich", Website = "http://www.fcbayern.de", Founded = new DateTime(1900, 2, 13), Nickname = "The Bavarians" },
                new Team() { Name = "Manchester City", Website = "http://www.mcfc.com", Founded = new DateTime(1880, 1, 1), Nickname = "The Citizens" },
                new Team() { Name = "Chelsea", Website = "https://www.chelseafc.com", Founded = new DateTime(1905, 3, 10), Nickname = "The Pensioners" },
                new Team() { Name = "Arsenal", Website = "http://www.arsenal.com/", Founded = new DateTime(1886, 1, 1), Nickname = "The Gunners" },
            };

            foreach (var team in teams)
            {
                context.Teams.Add(team);
            }

            context.SaveChanges();

            return teams;
        }

        private void LoadMatches(SportSystemDbContext context, List<Team> teams)
        {
            var matches = new List<Match>()
            {
                new Match() { AwayTeamId = teams[0].Id, HomeTeamId = teams[1].Id, Start = new DateTime(2015, 6, 13) },
                new Match() { AwayTeamId = teams[0].Id, HomeTeamId = teams[3].Id, Start = new DateTime(2015, 6, 14) },
                new Match() { AwayTeamId = teams[4].Id, HomeTeamId = teams[2].Id, Start = new DateTime(2015, 6, 15) },
                new Match() { AwayTeamId = teams[2].Id, HomeTeamId = teams[5].Id, Start = new DateTime(2015, 6, 16) },
                new Match() { AwayTeamId = teams[4].Id, HomeTeamId = teams[1].Id, Start = new DateTime(2015, 6, 17) },
                new Match() { AwayTeamId = teams[5].Id, HomeTeamId = teams[0].Id, Start = new DateTime(2015, 6, 18) },
                new Match() { AwayTeamId = teams[3].Id, HomeTeamId = teams[6].Id, Start = new DateTime(2015, 6, 12) },
                new Match() { AwayTeamId = teams[1].Id, HomeTeamId = teams[5].Id, Start = new DateTime(2015, 6, 11) },
                new Match() { AwayTeamId = teams[4].Id, HomeTeamId = teams[5].Id, Start = new DateTime(2015, 6, 10) },
                new Match() { AwayTeamId = teams[6].Id, HomeTeamId = teams[5].Id, Start = new DateTime(2015, 6, 19) },
                new Match() { AwayTeamId = teams[2].Id, HomeTeamId = teams[6].Id, Start = new DateTime(2015, 6, 20) },
            };

            foreach (var match in matches)
            {
                context.Matches.Add(match);
                context.SaveChanges();
            }
        }

        private void LoadPlayers(SportSystemDbContext context, List<Team> teams)
        {
            var players = new List<Player>()
            {
                new Player() { FullName = "Alexis Sanchez", TeamId = teams[2].Id, BirthDate = new DateTime(1982, 1, 3), Height = 1.65 },
                new Player() { FullName = "Arjen Robben", TeamId = teams[1].Id, BirthDate = new DateTime(1982, 1, 3), Height = 1.65 },
                new Player() { FullName = "Franck Ribery", TeamId = teams[0].Id, BirthDate = new DateTime(1982, 1, 3), Height = 1.65 },
                new Player() { FullName = "Wayne Rooney", TeamId = teams[0].Id, BirthDate = new DateTime(1982, 1, 3), Height = 1.65 },
                new Player() { FullName = "Lionel Messi", BirthDate = new DateTime(1982, 1, 13), Height = 1.65 },
                new Player() { FullName = "Theo Walcott", BirthDate = new DateTime(1983, 2, 17), Height = 1.75 },
                new Player() { FullName = "Cristiano Ronaldo", BirthDate = new DateTime(1984, 3, 16), Height = 1.85 },
                new Player() { FullName = "Aaron Lennon", BirthDate = new DateTime(1985, 4, 15), Height = 1.95 },
                new Player() { FullName = "Gareth Bale", BirthDate = new DateTime(1986, 5, 14), Height = 1.90 },
                new Player() { FullName = "Antonio Valencia", BirthDate = new DateTime(1987, 5, 23), Height = 1.82 },
                new Player() { FullName = "Robin van Persie", BirthDate = new DateTime(1988, 6, 13), Height = 1.84 },
                new Player() { FullName = "Ronaldinho", BirthDate = new DateTime(1989, 7, 30), Height = 1.87 },
            };

            foreach (var player in players)
            {
                context.Players.Add(player);
            }

            context.SaveChanges();
        }

        private void LoadComments(SportSystemDbContext context)
        {
        }

        private void LoadUserMatchBets(SportSystemDbContext context)
        {
        }
    }
}
