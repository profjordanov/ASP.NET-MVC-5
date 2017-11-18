namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public IEnumerable<ConciseSnippetViewModel> LatestSnippets { get; set; }

        public IEnumerable<LabelViewModel> BestLabels { get; set; }

        public IEnumerable<CommentViewModel> LatestComments { get; set; }
    }
}