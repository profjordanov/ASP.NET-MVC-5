using AutoMapper;
using StreamPowered.App.Models.ViewModels;
using StreamPowered.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StreamPowered.App.Controllers
{
    public class GenresController : BaseController
    {
        public GenresController(IStreamPoweredData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var genres = this.Data.Genres.All()
                .Select(g => new SelectListItem() { Text = g.Name, Value = g.Id.ToString() });
            return this.PartialView(genres);
        }

        public ActionResult Details(int id)
        {
            var genre = this.Data.Genres.Find(id);
            if (genre == null)
            {
                return this.HttpNotFound("The requested genre was not found in the system.");
            }

            var games = genre.Games
                .OrderBy(g => g.Title);
            var model = Mapper.Map<IEnumerable<ConciseGameViewModel>>(games);
            ViewBag.Genre = genre.Name;
            return View(model);
        }
    }
}