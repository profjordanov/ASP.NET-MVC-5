using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySchool.Models.VMs.Game
{
    public class ExploreGameViewModel
    {
        public IList<SmallGameViewModel> SmallGameViewModel { get; set; }
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int TotalPages { get; set; }

        public int StartPage => CurrentPage-Constants.DefaultShowPages>0?CurrentPage-Constants.DefaultShowPages:1;
        public int EndPage => CurrentPage + Constants.DefaultShowPages < TotalPages ? CurrentPage + Constants.DefaultShowPages : TotalPages;
    }
}
