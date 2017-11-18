namespace Snippy.App.Models.ViewModels
{
    using System;

    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public int SnippetId { get; set; }

        public string SnippetTitle { get; set; }
    }
}