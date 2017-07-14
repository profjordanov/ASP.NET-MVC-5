using System.ComponentModel.DataAnnotations;

namespace PlaySchool.Models.VMs.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [RegularExpression("^[A-Z][A-Za-z]+$", ErrorMessage = "First name must start with capital letter and contain only letters!")]
        [MaxLength(15, ErrorMessage = "First name must be maximum 15 letters!")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression("^[A-Z][A-Za-z]+$", ErrorMessage = "Last name must start with capital letter and contain only letters!")]
        [MaxLength(15, ErrorMessage = "Last name must be maximum 15 letters!")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(Constants.UsernameRegex,ErrorMessage = "Username must start with letter and contain only letters, digit or \"_\"")]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 symbols long")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}