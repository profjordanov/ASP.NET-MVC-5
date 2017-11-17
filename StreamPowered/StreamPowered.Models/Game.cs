namespace StreamPowered.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public Game()
        {
            this.ImageUrls = new HashSet<ImageUrl>();
            this.Ratings = new HashSet<Rating>();
            this.Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SystemRequirements { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public virtual ICollection<ImageUrl> ImageUrls { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
