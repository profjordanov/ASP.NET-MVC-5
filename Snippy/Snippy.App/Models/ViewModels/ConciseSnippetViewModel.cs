namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class ConciseSnippetViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ConciseLabelViewModel> Labels { get; set; }
    }
}