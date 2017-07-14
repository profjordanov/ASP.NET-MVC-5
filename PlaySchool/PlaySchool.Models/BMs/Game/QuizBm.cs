using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Models.EntityModels;

namespace PlaySchool.Models.BMs.Game
{
    public class QuizBm
    {
        public int QuizId { get; set; }
        public IList<QuestionBm> Questions { get; set; }
    }
}
