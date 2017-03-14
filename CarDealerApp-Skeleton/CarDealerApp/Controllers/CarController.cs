using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

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
    }
}