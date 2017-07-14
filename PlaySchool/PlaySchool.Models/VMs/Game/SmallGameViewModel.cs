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
    public class SmallGameViewModel
    {
        private string _description;

        [Key]
        public int GameId { get; set; }
        [Required]
        [RegularExpression(Constants.GameNameRegex, ErrorMessage = "Game Name must start with capital letter and contain only digits and letters")]
        [MinLength(2, ErrorMessage = "Game Name must be atleast 2 symbols")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Description must be atleast 3 symbols")]
        public string Description
        {
            get
            {
                //Get only first 100 symbols of the desc if it's more than 100 symbols!
                var result = this._description.Substring(0, _description.Length < 100 ? _description.Length : 100);
                return result.Length == 100 ? result + "..." : result;
            }
            set { this._description = value; }
        }
        [Required]
        public ApplicationUser Creator { get; set; }
        [Range(0, int.MaxValue)]
        [DisplayName("Total times played")]
        public int NumberPlayed { get; set; }
    }
}
