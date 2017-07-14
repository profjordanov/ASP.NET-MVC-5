using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("parts")]
    public class PartsController : Controller
    {
        private PartsService service;

        public PartsController()
        {
            this.service = new PartsService();
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            var vms = this.service.GetAddVm();
            return this.View(vms);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Name, Price, Quantity, SupllierId")] AddPartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddPart(bind);
                return this.Redirect("All");
            }
            var vms = this.service.GetAddVm();
            return this.View(vms);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult All()
        {
            IEnumerable<AllPartVm> vms = this.service.GetAllPartsVms();
            return this.View(vms);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            DeletePartVm vm = this.service.GetDeleteVm(id);
            return this.View(vm);
        }

        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete([Bind(Include = "PartId")] DeletePartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.DeletePart(bind);
                return this.Redirect("/comments/All");
            }
            DeletePartVm vm = this.service.GetDeleteVm(bind.PartId);
            return this.View(vm);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            EditPartVm vm = this.service.GetEditVm(id);
            return this.View(vm);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Price, Quantity")] EditPartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditPart(bind);
                return this.RedirectToAction("All");
            }
            EditPartVm vm = this.service.GetEditVm(bind.Id);
            return this.View(vm);
        }

    }
}