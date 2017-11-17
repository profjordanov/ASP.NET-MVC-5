namespace SportSystem.WebApp.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class TeamInputModel : IMapTo<Team>
    {
        [Required(ErrorMessage = "The {0} is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The {0} should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The {0} should be between {2} and {1}")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "The {0} is required!")]
        public DateTime? Founded { get; set; }

        [Required(ErrorMessage = "The {0} is required!")]
        [Url(ErrorMessage = "Invalid {0}")]
        public string Website { get; set; }

        public IEnumerable<int> PlayerIds { get; set; }
    }
}