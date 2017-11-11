namespace SoftUni.Blog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
