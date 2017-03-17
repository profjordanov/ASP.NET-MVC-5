using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class SuppliersService : Service
    {
        public IEnumerable<SupplierVm> GetAllSuppliersByType(string type)
        {
            IEnumerable<Supplier> suppliersWanted;
            if (type.ToLower() == "local")
            {
                suppliersWanted = this.Context.Suppliers.Where(sup => !sup.IsImporter);
            }
            else if (type.ToLower() == "importers")
            {
                suppliersWanted = this.Context.Suppliers.Where(sup => sup.IsImporter);

            }
            else
            {
                throw  new ArgumentException("Invalid argument!");
            }

            IEnumerable<SupplierVm> viewModel =
                Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierVm>>(suppliersWanted);
            return viewModel;
        }

        public IEnumerable<SupplierAllVm> GetAllSuppliersByTypeForUsers(string type)
        {
            IEnumerable<Supplier> suppliersWanted = this.GetSupplierModelsByType(type);

            IEnumerable<SupplierAllVm> viewModels =
                Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierAllVm>>(suppliersWanted);
            return viewModels;
        }

        private IEnumerable<Supplier> GetSupplierModelsByType(string type)
        {
            IEnumerable<Supplier> suppliersWanted;
            if (type == null)
            {
                suppliersWanted = this.Context.Suppliers;
            }
            else if(type.ToLower() == "local")
            {
                suppliersWanted = this.Context.Suppliers.Where(sup => !sup.IsImporter);
            }
            else if (type.ToLower() == "importers")
            {
                suppliersWanted = this.Context.Suppliers.Where(sup => sup.IsImporter);
            }
            else
            {
                throw new ArgumentException("Invalid argument for the type of the supplier!");
            }

            return suppliersWanted;
        }

        public void AddSupllier(AddSupplierBm bind, int id)
        {
            Supplier supplier = Mapper.Map<AddSupplierBm, Supplier>(bind);
            this.Context.Suppliers.Add(supplier);
            this.Context.SaveChanges();
            //this.AddLog
        }
    }
}