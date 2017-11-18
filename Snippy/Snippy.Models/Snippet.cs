namespace Snippy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Snippet
    {
        public Snippet()
        {
            this.Labels = new HashSet<Label>();
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required, MinLength(5)]
        public string Title { get; set; }

        [Required, MinLength(5)]
        public string Description { get; set; }

        [Required, MinLength(3)]
        public string Code { get; set; }

        [Required]
        public virtual Language Language { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        // TODO: Implement rating?

        public virtual ICollection<Label> Labels { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
