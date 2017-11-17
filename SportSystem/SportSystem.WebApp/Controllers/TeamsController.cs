namespace SportSystem.WebApp.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using SportSystem.Data;
    using SportSystem.Models;
    using SportSystem.WebApp.InputModels;
    using SportSystem.WebApp.ViewModels;

    [Authorize]
    public class TeamsController : BaseController
    {
        public TeamsController(ISportSystemData data)
            : base(data)
        {
        }
        
        public ActionResult Details(int id)
        {
            var team = this.Data.Teams
                .All()
                .Where(x => x.Id == id)
                .Project()
                .To<TeamDetailsViewModel>()
                .FirstOrDefault();

            var userId = this.User.Identity.GetUserId();
            team.UserHasVoted = this.Data.Votes.All().Any(x => x.TeamId == id && x.UserId == userId);
            return this.View(team);
        }

        public ActionResult Create()
        {
            this.LoadFreePlayers();
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var team = Mapper.Map<Team>(model);
                this.Data.Teams.Add(team);

                foreach (var playerId in model.PlayerIds)
                {
                    var player = this.Data.Players.Find(playerId);
                    player.TeamId = team.Id;
                    this.Data.SaveChanges();
                }

                this.Data.SaveChanges();

                return this.RedirectToAction("Details", new { id = team.Id });
            }

            this.LoadFreePlayers();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                if (!this.Data.Votes.All().Any(x => x.TeamId == model.TeamId && x.UserId == userId))
                {
                    var vote = new Vote()
                    {
                        TeamId = model.TeamId,
                        UserId = userId,
                        Value = 1
                    };
                    this.Data.Votes.Add(vote);
                    this.Data.SaveChanges();

                    var newVotes = this.Data.Votes.All().Where(x => x.TeamId == model.TeamId)
                        .Sum(x => x.Value);
                    return this.Json(newVotes);
                }
            }

            var votes = this.Data.Votes.All().Where(x => x.TeamId == model.TeamId)
                .Sum(x => x.Value);
            return this.Json(votes);
        }

        public ActionResult GetPlayerInput()
        {
            this.LoadFreePlayers();
            return this.PartialView("_CreatePlayerPartial");
        }

        private void LoadFreePlayers()
        {
            this.ViewBag.Players = this.Data.Players
                .All()
                .Where(x => !x.TeamId.HasValue)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.FullName
                })
                .ToList();
        }
    }
}