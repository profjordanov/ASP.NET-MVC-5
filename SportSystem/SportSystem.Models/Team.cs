namespace SportSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        private ICollection<Player> players;
        private ICollection<Match> awayMatches;
        private ICollection<Match> homeMatches;
        private ICollection<Vote> votes;

        public Team()
        {
            this.players = new HashSet<Player>();
            this.homeMatches = new HashSet<Match>();
            this.awayMatches = new HashSet<Match>();
            this.votes = new HashSet<Vote>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public DateTime? Founded { get; set; }

        public string Website { get; set; }

        public virtual ICollection<Player> Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public virtual ICollection<Match> AwayMatches
        {
            get { return this.awayMatches; }
            set { this.awayMatches = value; }
        }

        public virtual ICollection<Match> HomeMatches
        {
            get { return this.homeMatches; }
            set { this.homeMatches = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        } 
    }
}
