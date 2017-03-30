using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CameraBazaar.Models.Enitities;
using CameraBazaar.Models.ViewModels;
using CameraBazaar.Services;
using AuthenticationManager = CameraBazaar.Web.Security.AuthenticationManager;

namespace CameraBazaar.Web.Controllers
{
    [RoutePrefix("cameras")]
    public class CamerasController : Controller
    {
        private CamerasService service;

        public CamerasController()
        {
            this.service = new CamerasService();
        }

        [HttpGet]
        [Route("all")]
        [Route("~/")]
        public ActionResult All()
        {
            IEnumerable<ShortCameraVm> vms = this.service.GetAllCameras();

            return this.View(vms);
        }

        [HttpGet]
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            string sessionId = this.Request.Cookies.Get("sessionId")?.Value;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = AuthenticationManager.GetAuthenticatedUser(sessionId);
            DetailsCameraVm camera = this.service.GetDetailsVm(id, user);

            if (camera == null)
            {
                return HttpNotFound();
            }
            return View(camera);
        }

        [HttpGet, Route("create")]

        public ActionResult Create()
        {
            string sessionId = this.Request.Cookies.Get("sessionId")?.Value;
            if (!AuthenticationManager.IsAuthenticated(sessionId))
            {
                return this.RedirectToAction("Login", "User");
            }

            return this.View();
        }
    }
}