using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using MessageBoard.Models;
using MessageBoard.Services;
using System.Runtime.InteropServices;

namespace MessageBoard.Controllers
{
    public class PortfolioController : Controller
    {
        private PortfolioService service;

        public PortfolioController()
        {
            this.service = new PortfolioService();
        }

        public ActionResult Index()
        {
            IEnumerable<PictureVM> vms = this.service.GetPictureVms();
            return View(vms);
        }

    }
}
