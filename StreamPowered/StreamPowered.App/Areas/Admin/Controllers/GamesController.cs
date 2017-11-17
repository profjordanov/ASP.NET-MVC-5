using Microsoft.AspNet.Identity;
using StreamPowered.App.Areas.Admin.Models.BindingModels;
using StreamPowered.App.Controllers;
using StreamPowered.Data.UnitOfWork;
using StreamPowered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StreamPowered.App.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GamesController : BaseController
    {
        public GamesController(IStreamPoweredData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Add()
        {
            var genres = this.Data.Genres.All()
                .Select(g => new SelectListItem() { Text = g.Name, Value = g.Id.ToString() });
            ViewBag.Genres = genres;
            return this.View(new GameBindingModel());
        }

        public ActionResult AddImageUrl()
        {
            return this.PartialView("_AddImageUrl", new List<string>() { string.Empty });
        }

        [HttpPost]
        public ActionResult Add(GameBindingModel gameModel)
        {
            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            var genre = this.Data.Genres.Find(gameModel.GenreId);
            var imageUrls = gameModel.ImageUrls.Select(url => new ImageUrl() { Url = url }).ToList();
            var game = new Game()
            {
                Title = gameModel.Title,
                Description = gameModel.Description,
                SystemRequirements = gameModel.SystemRequirements,
                Genre = genre,
                Author = currentUser
            };

            this.Data.Games.Add(game);
            this.Data.SaveChanges();
            game.ImageUrls = imageUrls;
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", "Games", new { area = "", id = game.Id });
        }
    }
}