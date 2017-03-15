using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Customer, AllCustomerVm>();
                expression.CreateMap<Car, AllCarsVm>();
                expression.CreateMap<Supplier, SupplierVm>()
                    .ForMember(vm => vm.NumberOfPartsToSupply,
                        configurationExpression => configurationExpression.MapFrom(suplier => suplier.Parts.Count));
                expression.CreateMap<Part, PartVm>();
                expression.CreateMap<Car, CarVm>();
               expression.CreateMap<Sale, SaleVm>()
                   .ForMember(vm => vm.Price,
                       configurationExpression =>
                           configurationExpression.MapFrom(sale =>
                               sale.Car.Parts.Sum(part => part.Price)));
                expression.CreateMap<AddCustomerBm, Customer>();
                expression.CreateMap<Customer, EditCustomerVm>();
                expression.CreateMap<EditCustomerBm, Customer>();
                expression.CreateMap<Customer, EditCustomerVm>();
                expression.CreateMap<EditCustomerBm, EditCustomerVm>();
                expression.CreateMap<AddPartBm, Part>();
                expression.CreateMap<Part, AllPartVm>();
                expression.CreateMap<Part, DeletePartVm>();
                expression.CreateMap<Part, DeletePartVm>();
                expression.CreateMap<Part, EditPartVm>();
                expression.CreateMap<AddCarBm, Car>()
                    .ForMember(car => car.Parts, configurationExpression => configurationExpression.Ignore());


            });
        }
    }
}