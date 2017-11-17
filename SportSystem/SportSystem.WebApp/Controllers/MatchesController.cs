namespace SportSystem.WebApp.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using PagedList;

    using SportSystem.Common;
    using SportSystem.Data;
    using SportSystem.Models;
    using SportSystem.WebApp.InputModels;
    using SportSystem.WebApp.ViewModels;

    [Authorize]
    public class MatchesController : BaseController
    {
        public MatchesController(ISportSystemData data) : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            var matches = this.Data.Matches
                .All()
                .OrderBy(x => x.Start)
                .Project()
                .To<MatchViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.View(matches);
        }

        public ActionResult Details(int id)
        {
            var match = this.Data.Matches
                .All()
                .Where(x => x.Id == id)
                .Project()
                .To<MatchDetailsViewModel>()
                .FirstOrDefault();
            return this.View(match);
        }

        public ActionResult BetForAwayMatch(MatchAwayBetInputModel model)
        {
            return this.BetForMatch(model);
        }

        public ActionResult BetForHomeMatch(MatchHomeBetInputModel model)
        {
            return this.BetForMatch(model);
        }

        public ActionResult AddComment(CommentInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var comment = Mapper.Map<Comment>(model);
                comment.UserId = this.User.Identity.GetUserId();
                comment.CreatedOn = DateTime.Now;
                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var commentViewModel = this.Data.Comments
                    .All()
                    .Where(x => x.Id == comment.Id)
                    .Project()
                    .To<CommentViewModel>()
                    .FirstOrDefault();
                return this.PartialView("DisplayTemplates/CommentViewModel", commentViewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid comment!");
        }

        private ActionResult BetForMatch(MatchBetInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var matchBet = Mapper.Map<UserMatchBet>(model);
                matchBet.UserId = this.User.Identity.GetUserId();
                this.Data.UserMatchBets.Add(matchBet);
                this.Data.SaveChanges();

                var userMatchBets = this.Data.UserMatchBets
                    .All()
                    .Where(x => x.MatchId == model.MatchId);

                var betsViewModel = new BetsViewModel
                {
                    AwayBets = userMatchBets.Sum(x => x.AwayBet),
                    HomeBets = userMatchBets.Sum(x => x.HomeBet)
                };

                return this.Json(betsViewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid bets");
        }
    }
}