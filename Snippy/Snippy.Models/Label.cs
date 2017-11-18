using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snippy.Models
{
    public class Label
    {
        public Label()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }

        [Required, MinLength(2)]
        public string Text { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}
