namespace SportSystem.WebApp.ViewModels
{
    using System;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class MatchViewModel : IMapFrom<Match>
    {
        public int Id { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public DateTime Start { get; set; }
    }
}