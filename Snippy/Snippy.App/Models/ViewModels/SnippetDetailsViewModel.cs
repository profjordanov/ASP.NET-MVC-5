namespace Snippy.App.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class SnippetDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public LanguageViewModel Language { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public IEnumerable<ConciseLabelViewModel> Labels { get; set; }

        public IEnumerable<ConciseCommentViewModel> Comments { get; set; }
    }
}