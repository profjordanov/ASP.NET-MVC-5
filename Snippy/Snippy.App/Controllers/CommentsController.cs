namespace Snippy.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using Microsoft.AspNet.Identity;
    using Models.BindingModels;
    using Snippy.Models;
    using Models.ViewModels;
    using System.Net;

    public class CommentsController : BaseController
    {
        public CommentsController(ISnippyData data)
            : base(data)
        {
        }

        [Authorize]
        public ActionResult Add(int id, CommentBindingModel commentModel)
        {
            var snippet = this.Data.Snippets.Find(id);
            if (snippet == null)
            {
                return this.HttpNotFound("The requested snippet was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            var comment = new Comment() { Content = commentModel.Content, Snippet = snippet, Author = currentUser, CreationTime = DateTime.Now };
            snippet.Comments.Add(comment);
            this.Data.SaveChanges();

            var model = Mapper.Map<ConciseCommentViewModel>(comment);
            return PartialView("_Comment", model);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var comment = this.Data.Comments.Find(id);
            if (comment == null)
            {
                return this.HttpNotFound("The requested comment was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            if (comment.Author != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "The currently logged in user does not have sufficient privileges to delete this comment.");
            }

            var model = Mapper.Map<CommentViewModel>(comment);
            return this.View(model);
        }

        [Authorize, ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            var comment = this.Data.Comments.Find(id);

            if (comment == null)
            {
                return this.HttpNotFound("The requested comment was not found in the system.");
            }

            var currentUser = this.Data.Users.Find(this.User.Identity.GetUserId());
            if (comment.Author != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "The currently logged in user does not have sufficient privileges to delete this comment.");
            }

            int snippetId = comment.Snippet.Id;
            this.Data.Comments.Remove(comment);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", "Snippets", new { id = snippetId });
        }
    }
}