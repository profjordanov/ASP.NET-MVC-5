using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        // GET: Customers
        private CustomersService service;
        private CarDealerContext context;

        public CustomersController()
        {
            this.service = new CustomersService();
            this.context = new CarDealerContext();
        }

        [HttpGet]
        [Route("all/{order:regex(ascending|descending)}")]
        public ActionResult All(string order)
        {
            IEnumerable<AllCustomerVm> viewModels = this.service.GetAllOrderedCustomers(order);
            return this.View(viewModels);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult About(int id)
        {
            AboutCustomerVm vm = this.service.GetCustomerWithCatData(id);
            return this.View(vm);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Name, BirthDate")] AddCustomerBm bind)
        {
            this.service.AddCustomer(bind);
            return this.Redirect("all/ascending");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            EditCustomerVm vm = this.service.GetEditCustomerVm(id);
            return this.View(vm);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id,Name,Birthdate")] EditCustomerBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditCustomer(bind);
                return this.RedirectToAction("All", new {order = "ascending"});
            }

            return this.View(this.service.GetEditCustomerVm(bind.Id));

        }

    }
}