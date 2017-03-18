using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("cars")]
    public class CarController : Controller
    {
        private CarsService service;

        public CarController()
        {
            this.service = new CarsService();
        }
        [HttpGet]
        [Route("{make?}")]
        public ActionResult All(string make)
        {
            IEnumerable<AllCarsVm> modelCarVms = this.service.GetCarsFromGivenMakeInOrder(make);
            ViewBag.Make = make;
            return this.View(modelCarVms);
        }
        [HttpGet]
        [Route("{id}/parts")]
        public ActionResult About(int id)
        {
            AboutCarVm vm = this.service.GetCarWithParts(id);
            return View(vm);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            return this.View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance, Parts")]AddCarBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            if (this.ModelState.IsValid)
            {
                this.service.AddCar(bind);
                return this.RedirectToAction("All");
            }
            return this.View();
        }
    }
}