using System;

namespace StreamPowered.App.Models.ViewModels
{
    public class ReviewViewModel
    {
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public int GameId { get; set; }

        public string GameTitle { get; set; }
    }
}