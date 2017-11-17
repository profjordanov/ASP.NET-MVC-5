using AutoMapper;
using StreamPowered.App.Models.ViewModels;
using StreamPowered.Data.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StreamPowered.App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IStreamPoweredData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            const int HomePageItems = 5;
            var highestRatedGames = this.Data.Games.All()
                .OrderByDescending(g => g.Ratings.Average(r => r.Value))
                .ThenBy(g => g.Title)
                .Take(HomePageItems);
            var latestReviews = this.Data.Reviews.All()
                .OrderByDescending(r => r.CreationTime)
                .Take(HomePageItems);
            var model = new HomePageViewModel()
            {
                HighestRatedGames = Mapper.Map<IEnumerable<ConciseGameViewModel>>(highestRatedGames),
                LatestReviews = Mapper.Map<IEnumerable<ReviewViewModel>>(latestReviews)
            };

            return View(model);
        }
    }
}