using System.ComponentModel.DataAnnotations;

namespace PlaySchool.Models.VMs.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}