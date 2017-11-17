namespace SportSystem.Data
{
    using System;
    using System.Collections.Generic;

    using Bookmarks.Data.Repositories;

    using SportSystem.Models;

    public class SportSystemData : ISportSystemData
    {
        private ISportSystemDbContext context;
        private IDictionary<Type, object> repositories;

        public SportSystemData(ISportSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Match> Matches
        {
            get { return this.GetRepository<Match>(); }
        }

        public IRepository<Player> Players
        {
            get { return this.GetRepository<Player>(); }
        }

        public IRepository<Team> Teams
        {
            get { return this.GetRepository<Team>(); }
        }

        public IRepository<UserMatchBet> UserMatchBets
        {
            get { return this.GetRepository<UserMatchBet>(); }
        }

        public IRepository<Vote> Votes
        {
            get { return this.GetRepository<Vote>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                var repository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
