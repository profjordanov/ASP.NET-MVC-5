namespace Snippy.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Language
    {
        public Language()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }

        [Required, MinLength(1)]
        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}