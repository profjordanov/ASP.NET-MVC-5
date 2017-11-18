namespace Snippy.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using AutoMapper;
    using System.Collections.Generic;
    using Models.ViewModels;
    using System.Linq;

    public class LabelsController : BaseController
    {
        public LabelsController(ISnippyData data)
            : base(data)
        {
        }

        public ActionResult Details(int id)
        {
            var label = this.Data.Labels.Find(id);
            if (label == null)
            {
                return this.HttpNotFound("The requested label was not found in the system.");
            }

            var snippets = label.Snippets
                .OrderByDescending(s => s.CreationTime);
            var model = Mapper.Map<IEnumerable<ConciseSnippetViewModel>>(snippets);
            ViewBag.Label = label.Text;
            return View(model);
        }
    }
}