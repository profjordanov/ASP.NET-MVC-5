using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    public class SuppliersController : Controller
    {
        private SuppliersService service;

        public SuppliersController()
        {
            this.service = new SuppliersService();
        }

        [HttpGet]
        [Route("suppliers/{type}")]
        public ActionResult All(string type)
        {
            IEnumerable<SupplierVm> viewModels = this.service.GetAllSuppliersByType(type);
            return this.View(viewModels);

        }
        // GET: Suppliers
        public ActionResult Index()
        {
            return View();
        }
    }
}