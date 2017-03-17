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
    [RoutePrefix("Sales")]
    public class SalesController : Controller
    {
        private SalesService service;

        public SalesController()
        {
            this.service = new SalesService();
        }

        [HttpGet]
        [Route]
        public ActionResult All()
        {
            IEnumerable<SaleVm> vms = this.service.GetAllSales();
            return this.View(vms);
        }

        [HttpGet]
        [Route("{id}/")]
        public ActionResult About(int id)
        {
            SaleVm salevm = this.service.GetSale(id);
            return this.View(salevm);
        }

        [HttpGet]
        [Route("discounted/{percent?}/")]
        public ActionResult Discounted(double? percent)
        {
            IEnumerable<SaleVm> sales = this.service.GetDiscountedSales(percent);
            return this.View(sales);
        }

        [HttpGet]
        [Route("add/")]
        public ActionResult Add()
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "User");
            }

            AddSaleVm vm = this.service.GetSaleVm();
            return this.View(vm);
        }

        [HttpPost]
        [Route("add/")]
        public ActionResult Add([Bind(Include = "CustomerId, CarId, Discount")] AddSaleBm bind)
        {
            if (this.ModelState.IsValid)
            {
                AddSaleconfirmationVm confirmationVm = this.service.GetSaleConfirmationVm(bind);
                return this.RedirectToAction("AddConfirmation", confirmationVm);
            }

            AddSaleVm vm = this.service.GetSaleVm();
            return this.View(vm);
        }

        [HttpGet]
        [Route("AddConfirmation")]
        public ActionResult Addconfirmation(AddSaleconfirmationVm vm)
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "User");
            }

            return this.View(vm);
        }

        [HttpPost]
        [Route("AddConfirmation")]
        public ActionResult Addconfirmation(AddSaleBm bind)
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "User");
            }

            this.service.AddSale(bind);
            return this.RedirectToAction("All");
        }
    }
}