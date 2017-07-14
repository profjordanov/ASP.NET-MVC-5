using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Models.EntityModels;

namespace PlaySchool.Models.VMs.Game
{
    public class QuizViewModel
    {
        public int QuizId { get; set; }
        public IList<QuestionViewModel> Questions { get; set; }
        public bool IsPlayed { get; set; }
    }
}
