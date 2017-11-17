namespace SportSystem.WebApp.ViewModels
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IEnumerable<MatchViewModel> TopMatches { get; set; }

        public IEnumerable<TeamViewModel> BestTeams { get; set; }
    }
}