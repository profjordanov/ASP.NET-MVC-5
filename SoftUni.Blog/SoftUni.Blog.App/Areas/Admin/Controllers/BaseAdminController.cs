namespace SoftUni.Blog.App.Areas.Admin.Controllers
{
    using App.Controllers;
    using Data.UnitOfWork;
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class BaseAdminController : BaseController
    {
        public BaseAdminController(ISoftUniBlogData data)
            : base(data)
        {
        }
    }
}