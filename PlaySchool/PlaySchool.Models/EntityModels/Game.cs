using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Models.VMs.Account;

namespace PlaySchool.Models.EntityModels
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        [RegularExpression(Constants.GameNameRegex, ErrorMessage = "Game Name must start with capital letter and contain only digits and letters")]
        [MinLength(2, ErrorMessage = "Game Name must be atleast 2 symbols")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Description must be atleast 3 symbols")]
        public string Description { get; set; }
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        [Required]
        public virtual ApplicationUser Creator { get; set; }
        [Range(0, int.MaxValue)]
        public int NumberPlayed { get; set; }
        [Required]
        [RegularExpression(Constants.JsGameUrlRegex,ErrorMessage = "Javascript File Url is not valid")]
        public string JsUrl { get; set; }
    }
}
