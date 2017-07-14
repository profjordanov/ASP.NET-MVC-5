using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySchool.Models.EntityModels
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string QuestionTitle { get; set; }
        public string QuestionTip { get; set; }
        [Required]
        public virtual Quiz Quiz { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
