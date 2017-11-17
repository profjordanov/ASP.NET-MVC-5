namespace StreamPowered.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<StreamPoweredDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StreamPoweredDbContext context)
        {
            this.SeedUsers(context);
            this.SeedGenres(context);
            this.SeedGames(context);
            this.SeedRatings(context);
            this.SeedReviews(context);
        }

        private void SeedUsers(StreamPoweredDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole("Admin");

                roleManager.Create(role);
            }

            if (!context.Users.Any())
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                User user;
                user = new User() { UserName = "admin", Email = "admin@example.com" };
                userManager.Create(user, "adminPass123");
                userManager.AddToRole(user.Id, "Admin");
                user = new User() { UserName = "Student", Email = "Student@softuni.bg" };
                userManager.Create(user, "studentPass123");
                user = new User() { UserName = "fiLeV", Email = "fiLeV@gmail.com" };
                userManager.Create(user, "FiLevs!Sup3rS3cr37!P@ssw0rd");
            }
        }

        private void SeedGenres(StreamPoweredDbContext context)
        {
            if (!context.Genres.Any())
            {
                context.Genres.AddOrUpdate(
                g => g.Name,
                new Genre() { Name = "Action" },
                new Genre() { Name = "RPG" },
                new Genre() { Name = "Funny" },
                new Genre() { Name = "Strategy" },
                new Genre() { Name = "Adventure" },
                new Genre() { Name = "Casual" },
                new Genre() { Name = "Racing" },
                new Genre() { Name = "Sports" },
                new Genre() { Name = "Simulation" });
                context.SaveChanges();
            }
        }

        private void SeedGames(StreamPoweredDbContext context)
        {
            if (!context.Games.Any())
            {
                var admin = context.Users.FirstOrDefault(u => u.UserName == "admin");
                context.Games.AddOrUpdate(
                    g => g.Title,
                    new Game()
                    {
                        Title = "Counter-Strike: Global Offensive",
                        Description = @"Counter-Strike: Global Offensive (CS: GO) will expand upon the team-based action gameplay that it pioneered when it was launched 14 years ago. CS: GO features new maps, characters, and weapons and delivers updated versions of the classic CS content (de_dust, etc.).",
                        SystemRequirements = @"Windows
MINIMUM: 
OS: WindowsR 7/Vista/XP 
Processor: IntelR CoreT 2 Duo E6600 or AMD PhenomT X3 8750 processor or better 
Memory: 2 GB RAM 
Graphics: Video card must be 256 MB or more and should be a DirectX 9-compatible with support for Pixel Shader 3.0 
DirectX: Version 9.0c 
Hard Drive: 8 GB available space",
                        Genre = context.Genres.FirstOrDefault(g => g.Name == "Action"),
                        ImageUrls = new[] { new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/730/ss_34090867f1a02b6c17652ba9043e3f622ed985a9.600x338.jpg?t=1447694262" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/730/ss_1d30c9a215fd621e2fd74f40d93b71587bf6409c.600x338.jpg?t=1447694262" }, new ImageUrl() { Url = " http://cdn.akamai.steamstatic.com/steam/apps/730/ss_baa02e979cd3852e3c4182afcd603ab64e3502f9.600x338.jpg?t=1447694262" }, new ImageUrl() { Url = " http://cdn.akamai.steamstatic.com/steam/apps/730/ss_ffe584c163a2b16e9c1b733b1c8e2ba669fb1204.600x338.jpg?t=1447694262" } },
                        Author = admin
                    },
                    new Game()
                    {
                        Title = "Fallout 4",
                        Description = @"Bethesda Game Studios, the award-winning creators of Fallout 3 and The Elder Scrolls V: Skyrim, welcome you to the world of Fallout 4 - their most ambitious game ever, and the next generation of open-world gaming.",
                        SystemRequirements = @"MINIMUM: 
OS: Windows 7/8/10 (64-bit OS required) 
Processor: Intel Core i5-2300 2.8 GHz/AMD Phenom II X4 945 3.0 GHz or equivalent 
Memory: 8 GB RAM 
Graphics: NVIDIA GTX 550 Ti 2GB/AMD Radeon HD 7870 2GB or equivalent 
Hard Drive: 30 GB available space
RECOMMENDED: 
OS: Windows 7/8/10 (64-bit OS required) 
Processor: Intel Core i7 4790 3.6 GHz/AMD FX-9590 4.7 GHz or equivalent 
Memory: 8 GB RAM 
Graphics: NVIDIA GTX 780 3GB/AMD Radeon R9 290X 4GB or equivalent 
Hard Drive: 30 GB available space",
                        Genre = context.Genres.FirstOrDefault(g => g.Name == "Action"),
                        ImageUrls = new[] { new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/377160/ss_4733a1f56becbff21118435bd49561d0ed2392e7.600x338.jpg?t=1447358782" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/377160/ss_f7861bd71e6c0c218d8ff69fb1c626aec0d187cf.600x338.jpg?t=1447358782" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/377160/ss_910437ac708aed7c028f6e43a6224c633d086b0a.600x338.jpg?t=1447358782" } },
                        Author = admin
                    },
                    new Game()
                    {
                        Title = "Dota 2",
                        Description = @"Dota is a competitive game of action and strategy, played both professionally and casually by millions of passionate fans worldwide. Players pick from a pool of over a hundred heroes, forming two teams of five players.",
                        SystemRequirements = @"Windows
MINIMUM: 
OS: Windows 7 
Processor: Dual core from Intel or AMD at 2.8 GHz 
Memory: 4 GB RAM 
Graphics: nVidia GeForce 8600/9600GT, ATI/AMD Radeon HD2600/3600 
DirectX: Version 9.0c 
Network: Broadband Internet connection 
Hard Drive: 8 GB available space 
Sound Card: DirectX Compatible",
                        Genre = context.Genres.FirstOrDefault(g => g.Name == "Strategy"),
                        ImageUrls = new[] { new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/570/ss_09f21774b2309fcb67a2d9f8b385b47c48e985ff.600x338.jpg?t=1447883099" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/570/ss_2a951d65c6084004dcdc292d4944c0fb4a059624.600x338.jpg?t=1447883099" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/570/ss_6a426a8d2d15ce7d7d9077f81c95daf3257fe387.600x338.jpg?t=1447883099" } },
                        Author = admin
                    },
                    new Game()
                    {
                        Title = "Grand Theft Auto V",
                        Description = @"A young street hustler, a retired bank robber and a terrifying psychopath must pull off a series of dangerous heists to survive in a ruthless city in which they can trust nobody, least of all each other.",
                        SystemRequirements = @"MINIMUM: 
OS: Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1, Windows Vista 64 Bit Service Pack 2* (*NVIDIA video card recommended if running Vista OS) 
Processor: Intel Core 2 Quad CPU Q6600 @ 2.40GHz (4 CPUs) / AMD Phenom 9850 Quad-Core Processor (4 CPUs) @ 2.5GHz 
Memory: 4 GB RAM 
Graphics: NVIDIA 9800 GT 1GB / AMD HD 4870 1GB (DX 10, 10.1, 11) 
Hard Drive: 65 GB available space 
Sound Card: 100% DirectX 10 compatible
RECOMMENDED: 
OS: Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1 
Processor: Intel Core i5 3470 @ 3.2GHz (4 CPUs) / AMD X8 FX-8350 @ 4GHz (8 CPUs) 
Memory: 8 GB RAM 
Graphics: NVIDIA GTX 660 2GB / AMD HD 7870 2GB 
Hard Drive: 65 GB available space 
Sound Card: 100% DirectX 10 compatible",
                        Genre = context.Genres.FirstOrDefault(g => g.Name == "Adventure"),
                        ImageUrls = new[] { new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/271590/ss_ea78dfa1d7d81c3781287cab165f64ca70f1f2ea.600x338.jpg?t=1447687485" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/271590/ss_d1555f147b4667f70fac769985df629cbfda40b8.600x338.jpg?t=1447687485" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/271590/ss_680684304e38a9c58a55866cde99469ae8ef510c.600x338.jpg?t=1447687485" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/271590/ss_be2b9e45c671f95b8bc9fde58dbbd1154b0b633a.600x338.jpg?t=1447687485" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/271590/ss_54a59b51d9a3dbd5cf6b8d8745716b293633a50b.600x338.jpg?t=1447687485" } },
                        Author = admin
                    },
                    new Game()
                    {
                        Title = "Team Fortress 2",
                        Description = @"Nine distinct classes provide a broad range of tactical abilities and personalities. Constantly updated with new game modes, maps, equipment and, most importantly, hats!",
                        SystemRequirements = @"Windows
MINIMUM: 
OS: WindowsR 7 (32/64-bit)/Vista/XP 
Processor: 1.7 GHz Processor or better 
Memory: 512 MB RAM 
DirectX: Version 8.1 
Network: Broadband Internet connection 
Hard Drive: 15 GB available space 
Additional Notes: Mouse, Keyboard
RECOMMENDED: 
OS: WindowsR 7 (32/64-bit) 
Processor: Pentium 4 processor (3.0GHz, or better) 
Memory: 1 GB RAM 
DirectX: Version 9.0c 
Network: Broadband Internet connection 
Hard Drive: 15 GB available space 
Additional Notes: Mouse, Keyboard",
                        Genre = context.Genres.FirstOrDefault(g => g.Name == "Action"),
                        ImageUrls = new[] { new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/440/0000002574.600x338.jpg?t=1447886799" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/440/0000002575.600x338.jpg?t=1447886799" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/440/0000002576.600x338.jpg?t=1447886799" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/440/0000002577.600x338.jpg?t=1447886799" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/440/0000002579.600x338.jpg?t=1447886799" } },
                        Author = admin
                    },
                    new Game()
                    {
                        Title = "Garry's Mod",
                        Description = @"Garry's Mod is a physics sandbox. There aren't any predefined aims or goals. We give you the tools and leave you to play.",
                        SystemRequirements = @"Windows
MINIMUM:  
OS: WindowsR Vista/XP/2000 
Processor: 1.8 GHz Processor 
Memory: 2GB RAM 
Graphics: DirectXR 9 level Graphics Card (Requires support for SSE) 
Hard Drive: 1GB 
Other Requirements: Internet Connection
Mac OS X
MINIMUM: OS X version Snow Leopard 10.6.3, 2GB RAM, NVIDIA GeForce 8 or higher, ATI X1600 or higher, or Intel HD 3000 or higher Mouse, Keyboard, Internet Connection, Monitor",
                        Genre = context.Genres.FirstOrDefault(g => g.Name == "Funny"),
                        ImageUrls = new[] { new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/4000/ss_4162b10390d84aa600e5ed895fdc885482eb2e71.600x338.jpg?t=1421333577" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/4000/ss_ff27d52a103d1685e4981673c4f700b860cb23de.600x338.jpg?t=1421333577" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/4000/ss_65ec9828538eac8db20efc8149990060911fefc4.600x338.jpg?t=1421333577" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/4000/ss_c13ffded1d71bedfa7ede94c11cbd21fbd21a32c.600x338.jpg?t=1421333577" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/4000/0000000827.600x338.jpg?t=1421333577" } },
                        Author = admin
                    },
                    new Game()
                    {
                        Title = "Verdun",
                        Description = @"Verdun is the first multiplayer FPS set in a realistic First World War setting. The merciless trench warfare offers a unique battlefield experience, immersing you and your squad into intense battles of attack and defense.",
                        SystemRequirements = @"Windows
MINIMUM: 
OS: Windows Vista/7/8 
Processor: Intel Core2 Duo 2.4Ghz or Higher / AMD 3Ghz or Higher 
Memory: 3 GB RAM 
Graphics: Geforce GTX 960M / Radeon HD 7750 or higher, 1GB video card memory 
DirectX: Version 9.0c 
Network: Broadband Internet connection 
Hard Drive: 12 GB available space 
Additional Notes: Multiplayer only, make sure you have a stable and fast internet connection.
RECOMMENDED: 
Memory: 4 GB RAM 
Graphics: 2GB video card memory",
                        Genre = context.Genres.FirstOrDefault(g => g.Name == "Strategy"),
                        ImageUrls = new[] { new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/242860/ss_e86e8ce863bd67f5fcc5f03b1f4cf75a76f711b6.600x338.jpg?t=1448057367" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/242860/ss_47d9116d5268cf8a64c452cb0f26809a9eaec2e5.600x338.jpg?t=1448057367" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/242860/ss_d0aea9deb102217936445c11b930b915d974e4e3.600x338.jpg?t=1448057367" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/242860/ss_ba7a42fc33abe1b3a616531bbd65aab2e4cb9af4.600x338.jpg?t=1448057367" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/242860/ss_bba495d434a53719c0ff80b77f179c09979e8a09.600x338.jpg?t=1448057367" }, new ImageUrl() { Url = "http://cdn.akamai.steamstatic.com/steam/apps/242860/ss_a70d9b23cbd9a7241bdb2795a678fcc2c39d3df4.600x338.jpg?t=1448057367" } },
                        Author = admin
                    });
                context.SaveChanges();
            }
        }

        private void SeedRatings(StreamPoweredDbContext context)
        {
            if (!context.Ratings.Any())
            {
                context.Ratings.AddOrUpdate(
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Counter-Strike: Global Offensive")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                    Value = 4,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Counter-Strike: Global Offensive")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Counter-Strike: Global Offensive")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Fallout 4")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 4,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Fallout 4")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 2,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Fallout 4")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Dota 2")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                    Value = 4,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Dota 2")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Dota 2")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 4,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Grand Theft Auto V")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Grand Theft Auto V")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Grand Theft Auto V")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Team Fortress 2")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 3,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Team Fortress 2")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                    Value = 4,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Team Fortress 2")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Garry's Mod")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Garry's Mod")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Garry's Mod")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Verdun")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Verdun")
                },
                new Rating()
                {
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                    Value = 5,
                    Game = context.Games.FirstOrDefault(g => g.Title == "Verdun")
                });
                context.SaveChanges();
            }
        }

        private void SeedReviews(StreamPoweredDbContext context)
        {
            if (!context.Reviews.Any())
            {
                context.Reviews.AddOrUpdate(
                    new Review()
                    {
                        Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                        CreationTime = new DateTime(2015, 1, 21),
                        Content = "i recommend this game",
                        Game = context.Games.FirstOrDefault(g => g.Title == "Counter-Strike: Global Offensive")
                    },
                    new Review()
                    {
                        Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                        CreationTime = new DateTime(2014, 3, 12),
                        Content = "The good CS with a lot of new benefits and bonuses",
                        Game = context.Games.FirstOrDefault(g => g.Title == "Counter-Strike: Global Offensive")
                    },
                    new Review()
                    {
                        Author = context.Users.FirstOrDefault(u => u.UserName == "Student"),
                        CreationTime = new DateTime(2014, 10, 10),
                        Content = "It's like Dota 2 but with less wizards and more Russians.",
                        Game = context.Games.FirstOrDefault(g => g.Title == "Counter-Strike: Global Offensive")
                    },
                    new Review()
                    {
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreationTime = new DateTime(2015, 9, 14),
                        Content = "I have lost 10lbs since starting Fallout 4 because I keep forgetting to eat. 10/10 - best fitness game on Steam.",
                        Game = context.Games.FirstOrDefault(g => g.Title == "Fallout 4")
                    },
                    new Review()
                    {
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreationTime = new DateTime(2015, 9, 14),
                        Content = @"Don't worry, the people who enjoy the game are too busy playing the game to write the positive reviews.

edit: how is this review helpful

also for those who are calling me ""bethedrone"" or w/e I'm not defending the <3 aspects of it. Fallout 4 is a good game, but it certainly isn't the same as the previous games. It got a bit dumbed down in terms of the rpg mechanic, sure, but that doesnt mean the game is bad. I honestly had low hopes for the game and ended up pleasantly surprised.",
                        Game = context.Games.FirstOrDefault(g => g.Title == "Fallout 4")
                    },
                    new Review()
                    {
                        Author = context.Users.FirstOrDefault(u => u.UserName == "fiLeV"),
                        CreationTime = new DateTime(2015, 5, 22),
                        Content = "one of few games that has stood the test of time and been tended well by devlopers. maybe if everyone just abandonded this we'd get HL3 already though. I suppose Valves thinking on it is \"well we have one successfull game still, why bother making another. We use to make games, now we just make money\"",
                        Game = context.Games.FirstOrDefault(g => g.Title == "Team Fortress 2")
                    });
                context.SaveChanges();
            }
        }
    }
}