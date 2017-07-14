using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PlaySchool.Models;

namespace PlaySchool.Views.Admin
{
    public class PromoteUserViewModel
    {
        [Required]
        [RegularExpression(Constants.UsernameRegex, ErrorMessage = "Username must start with letter and contain only letters, digit or \"_\"")]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 symbols long")]
        public string Username { get; set; }
    }
}