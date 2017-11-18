using System;
using System.ComponentModel.DataAnnotations;

namespace Snippy.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        [Required, MinLength(5)]
        public string Content { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public virtual Snippet Snippet { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
    }
}