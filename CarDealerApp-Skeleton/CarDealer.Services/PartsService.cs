using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class PartsService : Service
    {
        public IEnumerable<AddPartSupplierVm> GetAddVm()
        {
            return this.Context.Suppliers.Select(supplier => new AddPartSupplierVm()
            {
                Id = supplier.Id,
                Name = supplier.Name
            });
        }

        public void AddPart(AddPartBm bind)
        {
            Part part = Mapper.Map<AddPartBm, Part>(bind);
            Supplier wantedSupplier = this.Context.Suppliers.Find(bind.SupplierId);
            part.Supplier = wantedSupplier;
            if (part.Quantity == 0)
            {
                part.Quantity = 1;
            }
            this.Context.Parts.Add(part);
            this.Context.SaveChanges();
        }

        public IEnumerable<AllPartVm> GetAllPartsVms()
        {
            IEnumerable<Part> parts = this.Context.Parts;
            IEnumerable<AllPartVm> vms = Mapper.Map<IEnumerable<Part>, IEnumerable<AllPartVm>>(parts);
            return vms;
        }
    }
}
