using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlaySchool.Models.BMs.Admin;
using PlaySchool.Services.Interfaces;
using PlaySchool.Views.Admin;

namespace PlaySchool.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Home")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private IAdminHomeService service;
        public HomeController(IAdminHomeService service)
        {
            this.service = service;
        }
        // GET: Admin/Home/Index
        [Route("Start",Name = "admindefault")]
        [HttpGet]
        public ActionResult Start()
        {
            return View();
        }
        // GET: Admin/Home/PromoteUser
        [Route("PromoteUser")]
        [HttpGet]
        public ActionResult PromoteUser()
        {
            return View();
        }
        // POST: Admin/Home/PromoteUser
        [Route("PromoteUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromoteUser([Bind(Include = "Username")]PromoteUserBm bind)
        {
            if (ModelState.IsValid)
            {
                string msg = service.PromoteUserToAdmin(bind);
                ViewBag.Message = msg;
            }
            return View(new PromoteUserViewModel() {Username = bind.Username});
        }
    }
}