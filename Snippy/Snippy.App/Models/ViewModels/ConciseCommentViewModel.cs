namespace Snippy.App.Models.ViewModels
{
    using System;

    public class ConciseCommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }
    }
}