namespace StreamPowered.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "The rating should be an integer number between 1 and 5")]
        public int Value { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public virtual Game Game { get; set; }
    }
}