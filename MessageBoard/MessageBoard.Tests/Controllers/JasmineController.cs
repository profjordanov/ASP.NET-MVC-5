using System;
using System.Web.Mvc;

namespace MessageBoard.Tests.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
