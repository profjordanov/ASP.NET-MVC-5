using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySchool.Models.VMs.Account
{
    public class ProfileViewModel
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
        [RegularExpression(Constants.UsernameRegex, ErrorMessage = "Username must start with letter and contain only letters, digit or \"_\"")]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 symbols long")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Range(0,10000,ErrorMessage = "The count of points must be between 0 and 10,000!")]
        [Display(Name = "Points")]
        public int Points { get; set; }
        [Required]
        public string ProfilePictureName { get; set; }
    }
}
