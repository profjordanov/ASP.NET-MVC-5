using System.Collections.Generic;

namespace StreamPowered.App.Models.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<ConciseGameViewModel> HighestRatedGames { get; set; }

        public IEnumerable<ReviewViewModel> LatestReviews { get; set; }
    }
}