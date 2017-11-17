using System.Web.Mvc;
using StreamPowered.Data.UnitOfWork;

namespace StreamPowered.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private IStreamPoweredData data;

        protected BaseController(IStreamPoweredData data)
        {
            this.Data = data;
        }

        protected IStreamPoweredData Data { get; private set; }
    }
}