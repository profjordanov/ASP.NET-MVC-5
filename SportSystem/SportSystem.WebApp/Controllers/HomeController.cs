namespace SportSystem.WebApp.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using SportSystem.Common;
    using SportSystem.Data;
    using SportSystem.WebApp.ViewModels;

    public class HomeController : BaseController
    {
        public HomeController(ISportSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {

            var homeViewModel = new HomeViewModel()
            {
                BestTeams = this.Data.Teams.All()
                    .OrderByDescending(x => x.Votes.Count())
                    .Take(GlobalConstants.NumberOfTeamOnHomePage)
                    .Project()
                    .To<TeamViewModel>(),
                TopMatches = this.Data.Matches.All()
                    .OrderByDescending(x => x.UserMatchBets.Sum(s => s.AwayBet) + x.UserMatchBets.Sum(s => s.HomeBet))
                    .Take(GlobalConstants.NumberOfMatchesOnHomePage)
                    .Project()
                    .To<MatchViewModel>()
            };

            return View(homeViewModel);
        }
    }
}