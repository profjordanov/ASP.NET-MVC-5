using AutoMapper;
using StreamPowered.App.Models.ViewModels;
using StreamPowered.Data.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace StreamPowered.App.Controllers
{
    public class GamesController : BaseController
    {
        public GamesController(IStreamPoweredData data)
            : base(data)
        {
        }

        public ActionResult Index(int page = 1, int count = 5)
        {
            var games = this.Data.Games.All()
                .OrderBy(g => g.Title)
                .Skip((page - 1) * count)
                .Take(count);
            ViewBag.MaxPages = (this.Data.Games.All().Count() + count - 1) / count;
            ViewBag.CurrentPage = page;
            var model = Mapper.Map<IEnumerable<ConciseGameViewModel>>(games);

            return this.View(model);
        }

        public ActionResult Details(int id)
        {
            var game = this.Data.Games.All()
                .Include(g => g.Genre)
                .Include(g => g.Reviews)
                .Include(g => g.Ratings)
                .FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return this.HttpNotFound("The requested game was not found in the system.");
            }

            var model = Mapper.Map<GameDetailsViewModel>(game);
            return this.View(model);
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}