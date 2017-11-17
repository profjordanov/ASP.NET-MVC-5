using AutoMapper;
using Microsoft.AspNet.Identity;
using StreamPowered.App.Models.BindingModels;
using StreamPowered.App.Models.ViewModels;
using StreamPowered.Data.UnitOfWork;
using StreamPowered.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StreamPowered.App.Controllers
{
    public class RatingsController : BaseController
    {
        public RatingsController(IStreamPoweredData data)
            : base(data)
        {
        }

        [Authorize]
        public ActionResult RatingDetails(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return this.HttpNotFound("The requested game was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            var existingRating = this.Data.Ratings.All().FirstOrDefault(r => r.Game.Id == game.Id && r.Author.Id == currentUser.Id);
            if (existingRating != null)
            {
                var model = Mapper.Map<RatingViewModel>(existingRating);
                return this.PartialView("_Rating", model);
            }
            else
            {
                return this.PartialView("_AddRating", new RatingBindingModel() { GameId = id });
            }
        }

        [Authorize]
        public ActionResult Add(int id, RatingBindingModel ratingModel)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return this.HttpNotFound("The requested game was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            var existingRating = this.Data.Ratings.All().FirstOrDefault(r => r.Game.Id == game.Id && r.Author.Id == currentUser.Id);
            if (existingRating != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You have already rated this game");
            }

            var rating = new Rating() { Value = ratingModel.Value, Game = game, Author = currentUser };
            game.Ratings.Add(rating);
            this.Data.SaveChanges();

            var model = Mapper.Map<RatingViewModel>(rating);
            return this.PartialView("_Rating", model);
        }
    }
}