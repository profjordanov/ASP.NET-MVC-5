using AutoMapper;
using Microsoft.AspNet.Identity;
using StreamPowered.App.Models.BindingModels;
using StreamPowered.App.Models.ViewModels;
using StreamPowered.Data.UnitOfWork;
using StreamPowered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StreamPowered.App.Controllers
{
    public class ReviewsController : BaseController
    {
        public ReviewsController(IStreamPoweredData data)
            :base(data)
        {
        }

        [Authorize]
        public ActionResult Add(int id, ReviewBindingModel reviewModel)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return this.HttpNotFound("The requested game was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            var review = new Review() { Content = reviewModel.Content, Game = game, Author = currentUser, CreationTime = DateTime.Now };
            game.Reviews.Add(review);
            this.Data.SaveChanges();

            var model = Mapper.Map<ConciseReviewViewModel>(review);
            return this.PartialView("_Review", model);
        }
    }
}