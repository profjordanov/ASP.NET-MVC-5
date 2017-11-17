using System.ComponentModel.DataAnnotations;
namespace SportSystem.Models
{
    public class UserMatchBet
    {
        public int Id { get; set; }

        public decimal HomeBet { get; set; }

        public decimal AwayBet { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match { get; set; }
    }
}
