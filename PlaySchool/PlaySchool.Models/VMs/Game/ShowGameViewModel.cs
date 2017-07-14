using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Models.EntityModels;

namespace PlaySchool.Models.VMs.Game
{
    public class ShowGameViewModel
    {
        [Required]
        [RegularExpression(Constants.GameNameRegex,ErrorMessage = "Game Name must start with capital letter and contain only digits and letters")]
        [MinLength(2,ErrorMessage = "Game Name must be atleast 2 symbols")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Description must be atleast 3 symbols")]
        public string Description { get; set; }
        [Required]
        public ApplicationUser Creator { get; set; }
        [Range(0,int.MaxValue)]
        [DisplayName("Total times played")]
        public int NumberPlayed { get; set; }
        public Quiz Quiz { get; set; }
        [Required]
        [RegularExpression(Constants.JsGameUrlRegex, ErrorMessage = "Javascript File Url is not valid")]
        public string JsUrl { get; set; }
    }
}
