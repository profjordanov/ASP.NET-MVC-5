using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySchool.Models.BMs.Account
{
    public class ChangeNameBm
    {
        [Display(Name = "First Name")]
        [RegularExpression("^[A-Z][A-Za-z]+$", ErrorMessage = "First name must start with capital letter and contain only letters!")]
        [MaxLength(15, ErrorMessage = "First name must be maximum 15 letters!")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression("^[A-Z][A-Za-z]+$", ErrorMessage = "Last name must start with capital letter and contain only letters!")]
        [MaxLength(15, ErrorMessage = "Last name must be maximum 15 letters!")]
        public string LastName { get; set; }
    }
}
