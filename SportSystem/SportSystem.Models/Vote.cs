namespace SportSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
