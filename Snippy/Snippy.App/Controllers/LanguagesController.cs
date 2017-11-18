namespace Snippy.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.ViewModels;

    public class LanguagesController : BaseController
    {
        public LanguagesController(ISnippyData data)
            : base(data)
        {
        }

        public ActionResult Details(int id)
        {
            var language = this.Data.Languages.Find(id);
            if (language == null)
            {
                return this.HttpNotFound("The requested language was not found in the system.");
            }

            var snippets = language.Snippets
                .OrderByDescending(s => s.CreationTime);
            var model = Mapper.Map<IEnumerable<ConciseSnippetViewModel>>(snippets);
            ViewBag.Language = language.Name;
            return View(model);
        }
    }
}