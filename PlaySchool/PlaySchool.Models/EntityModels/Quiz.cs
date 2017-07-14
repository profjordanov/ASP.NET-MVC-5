using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySchool.Models.EntityModels
{
    [Table("Quizzes")]
    public class Quiz
    {
        public Quiz()
        {
            this.Questions = new List<Question>();
        }
        [Key]
        public int QuizId { get; set; }
        public virtual Game Game { get; set; }
        [Required]
        public virtual ApplicationUser Creator { get; set; }
        public virtual IList<Question> Questions { get; set; }
        [Column("QuizId")]
        public virtual IList<Player> PlayersPlayed { get; set; }
    }
}
