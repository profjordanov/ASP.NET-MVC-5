using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("suppliers")]
    public class SuppliersController : Controller
    {
        private SuppliersService service;

        public SuppliersController()
        {
            this.service = new SuppliersService();
        }

        [HttpGet]
        [Route("{type:regex(local|importers)?}")]
        public ActionResult All(string type)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                IEnumerable<SupplierVm> viewModels = this.service.GetAllSuppliersByType(type);
                return this.View(viewModels);
            }
            User user = AuthenticationManager.GetAuthenticatedUser(httpCookie.Value);
            ViewBag.Username = user.Username;
            IEnumerable<SupplierAllVm> vm = this.service.GetAllSuppliersByTypeForUsers(type);
            return this.View("AllSuppliersForUser", vm);

        }

        [HttpGet]
        [Route("add/")]
        public ActionResult Add()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All");
            }

            return this.View();
        }

        [HttpPost]
        [Route("add/")]
        public ActionResult Add([Bind(Include = "Name, IsImporter")] AddSupplierBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All");
            }
            User loggedInUser = AuthenticationManager.GetAuthenticatedUser(httpCookie.Value);

            this.service.AddSupllier(bind, loggedInUser.Id);
            return this.RedirectToAction("All");

        }
    }
}