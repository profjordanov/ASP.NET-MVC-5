using System.ComponentModel.DataAnnotations;

namespace PlaySchool.Models.VMs.Account
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression(Constants.UsernameRegex, ErrorMessage = "Username must start with letter and contain only letters, digit or \"_\"")]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 symbols long")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}