namespace SoftUni.Blog.App.Controllers
{
    using Data.UnitOfWork;
    using Blog.Models;
    using System.Web.Mvc;
    using System;
    using System.Web.Routing;
    using System.Linq;

    public class BaseController : Controller
    {
        public BaseController(ISoftUniBlogData data)
        {
            this.Data = data;
        }

        public BaseController(ISoftUniBlogData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        public ISoftUniBlogData Data { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}