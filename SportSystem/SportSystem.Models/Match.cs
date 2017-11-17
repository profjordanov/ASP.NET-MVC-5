namespace SportSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Match
    {
        private ICollection<Comment> comments;
        private ICollection<UserMatchBet> userMatchBets;

        public Match()
        {
            this.comments = new HashSet<Comment>();
            this.userMatchBets = new HashSet<UserMatchBet>();
        }

        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        public DateTime Start { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<UserMatchBet> UserMatchBets
        {
            get { return this.userMatchBets; }
            set { this.userMatchBets = value; }
        }
    }
}
