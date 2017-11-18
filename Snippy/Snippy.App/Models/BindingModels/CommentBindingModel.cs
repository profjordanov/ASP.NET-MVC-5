namespace Snippy.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CommentBindingModel
    {
        public int SnippetId { get; set; }

        [Required, MinLength(5)]
        public string Content { get; set; }
    }
}