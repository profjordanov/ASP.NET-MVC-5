namespace Snippy.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.ViewModels;

    public class HomeController : BaseController
    {
        private const int ItemsInHomePage = 5;

        public HomeController(ISnippyData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var latestSnippets = this.Data.Snippets.All()
                .OrderByDescending(s => s.CreationTime)
                .Take(ItemsInHomePage);
            var bestLabels = this.Data.Labels.All()
                .OrderByDescending(l => l.Snippets.Count())
                .Take(ItemsInHomePage);
            var latestComments = this.Data.Comments.All()
                .OrderByDescending(c => c.CreationTime)
                .Take(ItemsInHomePage);
            var model = new HomePageViewModel()
            {
                BestLabels = Mapper.Map<IEnumerable<LabelViewModel>>(bestLabels),
                LatestSnippets = Mapper.Map<IEnumerable<ConciseSnippetViewModel>>(latestSnippets),
                LatestComments = Mapper.Map<IEnumerable<CommentViewModel>>(latestComments)
            };

            return this.View(model);
        }
    }
}