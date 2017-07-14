using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySchool.Models.VMs.Account
{
    public class AccountSearchViewModel
    {
        [Required]
        [RegularExpression(Constants.UsernameRegex, ErrorMessage = "Username must start with letter and contain only letters, digit or \"_\"")]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 symbols long")]
        public string Username { get; set; }
    }
}
