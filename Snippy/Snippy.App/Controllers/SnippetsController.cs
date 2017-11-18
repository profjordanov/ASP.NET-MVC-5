namespace Snippy.App.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.ViewModels;
    using Models.BindingModels;
    using Snippy.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Net;

    public class SnippetsController : BaseController
    {
        public SnippetsController(ISnippyData data)
            : base(data)
        {
        }

        public ActionResult Index(int page = 1, int count = 5)
        {
            var snippets = this.Data.Snippets.All()
                .OrderByDescending(s => s.CreationTime)
                .Skip((page - 1) * count)
                .Take(count);
            ViewBag.MaxPages = (this.Data.Snippets.All().Count() + count - 1) / count;
            ViewBag.CurrentPage = page;
            var model = Mapper.Map<IEnumerable<ConciseSnippetViewModel>>(snippets);
            return this.View(model);
        }

        [Authorize]
        public ActionResult MySnippets()
        {
            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            var snippets = currentUser.Snippets;
            var model = Mapper.Map<IEnumerable<ConciseSnippetViewModel>>(snippets);
            return this.View(model);
        }

        public ActionResult Details(int id)
        {
            var snippet = this.Data.Snippets.Find(id);
            if (snippet == null)
            {
                return this.HttpNotFound("The requested snippet was not found in the system.");
            }

            var model = Mapper.Map<SnippetDetailsViewModel>(snippet);
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            this.PassLanguagesToView();
            return this.View();
        }

        [HttpPost]
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Add(SnippetBindingModel snippetModel)
        {
            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            var snippet = Mapper.Map<Snippet>(snippetModel);
            var language = this.Data.Languages.Find(snippetModel.LanguageId);
            if (language == null)
            {
                return this.HttpNotFound("The requested language was not found in the system.");
            }

            snippet.Author = currentUser;
            snippet.CreationTime = DateTime.Now;
            snippet.Language = language;
            snippet.Labels = this.GetOrUpdateLabels(snippet.Labels);
            this.Data.Snippets.Add(snippet);
            this.Data.SaveChanges();
            return this.RedirectToAction("Details", new { id = snippet.Id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var snippet = this.Data.Snippets.Find(id);
            if (snippet == null)
            {
                return this.HttpNotFound("The requested snippet was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            if (snippet.Author != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "The currently logged in user does not have sufficient privileges to edit this snippet.");
            }

            this.PassLanguagesToView();
            var model = Mapper.Map<SnippetBindingModel>(snippet);
            return this.View(model);
        }

        [HttpPost]
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SnippetBindingModel snippetModel)
        {
            var snippet = this.Data.Snippets.Find(id);
            if (snippet == null)
            {
                return this.HttpNotFound("The requested snippet was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            if (snippet.Author != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "The currently logged in user does not have sufficient privileges to edit this snippet.");
            }

            var snippetLabels = Mapper.Map<Snippet>(snippetModel).Labels;
            snippet.Id = id;
            snippet.Language = this.Data.Languages.Find(snippetModel.LanguageId);
            snippet.Labels = this.GetOrUpdateLabels(snippetLabels);
            this.Data.Snippets.Update(snippet);
            this.Data.SaveChanges();
            return this.RedirectToAction("Details", new { id = snippet.Id });
        }

        private void PassLanguagesToView()
        {
            var languages = this.Data.Languages.All();
            ViewBag.Languages = languages.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });
        }

        private List<Label> GetOrUpdateLabels(IEnumerable<Label> labels)
        {
            var dbLabels = new List<Label>();
            foreach (var label in labels)
            {
                var dbLabel = this.Data.Labels.All().FirstOrDefault(l => l.Text == label.Text);
                if (dbLabel == null)
                {
                    dbLabel = this.Data.Labels.Add(new Label() { Text = label.Text });
                    this.Data.SaveChanges();
                }

                dbLabels.Add(dbLabel);
            }

            return dbLabels;
        }

    }
}