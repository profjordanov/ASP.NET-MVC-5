using System.Collections.Generic;

namespace StreamPowered.App.Models.ViewModels
{
    public class GameDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SystemRequirements { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public double Rating { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }

        public IEnumerable<ConciseReviewViewModel> Reviews { get; set; }
    }
}