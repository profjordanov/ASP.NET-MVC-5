namespace Snippy.App.Controllers
{
    using Data.UnitOfWork;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        private ISnippyData data;

        protected BaseController(ISnippyData data)
        {
            this.Data = data;
        }

        protected ISnippyData Data { get; private set; }
    }
}