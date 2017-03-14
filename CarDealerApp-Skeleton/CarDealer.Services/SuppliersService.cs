using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
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
    }
}