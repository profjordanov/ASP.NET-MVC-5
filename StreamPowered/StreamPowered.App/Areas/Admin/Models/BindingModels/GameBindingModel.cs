using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StreamPowered.App.Areas.Admin.Models.BindingModels
{
    public class GameBindingModel
    {
        public GameBindingModel()
        {
            this.ImageUrls = new List<string>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "System requirements")]
        public string SystemRequirements { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Display(Name = "Images")]
        public IEnumerable<string> ImageUrls { get; set; }
    }
}