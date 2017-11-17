namespace SportSystem.WebApp.Controllers
{
    using System.Web.Mvc;

    using SportSystem.Data;

    public abstract class BaseController : Controller
    {
        protected BaseController(ISportSystemData data)
        {
            this.Data = data;
        }

        protected ISportSystemData Data { get; private set; }
    }
}