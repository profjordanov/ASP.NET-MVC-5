using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySchool.Models.VMs.Game
{
    public class QuestionViewModel
    {
        public string QuestionTitle { get; set; }
        public string QuestionTip { get; set; }
        [Required(AllowEmptyStrings = true,ErrorMessage = "You have empty answer box! All questions have answers!"),StringLength(100,ErrorMessage = "Too much symbols for answer! Try smaller!")]
        public string Answer { get; set; }
    }
}
