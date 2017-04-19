using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MessageBoard.Models;

namespace MessageBoard.Data
{

    public class MessageBoardContext : DbContext
    {
        public MessageBoardContext()
          : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
              new MigrateDatabaseToLatestVersion<MessageBoardContext, MessageBoardMigrationsConfiguration>()
              );
        }

        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }

    }
}