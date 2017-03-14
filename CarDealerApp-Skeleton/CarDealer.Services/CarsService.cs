using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class CarsService : Service
    {
        public IEnumerable<AllCarsVm> GetCarsFromGivenMakeInOrder(string make)
        {
            IEnumerable<Car> cars;
            if (make == null)
            {
                cars = this.Context.Cars
                .OrderBy(car => car.Make).ThenByDescending(car => car.TravelledDistance);
            }
            else
            {
                cars = this.Context.Cars.Where(car => car.Make.Contains(make))
                .OrderBy(car => car.Make).ThenByDescending(car => car.TravelledDistance);
            }
            IEnumerable<AllCarsVm> viewModel = Mapper.Instance.Map<IEnumerable<Car>, IEnumerable<AllCarsVm>>(cars);


            return viewModel;
        }

        public AboutCarVm GetCarWithParts(int id)
        {
            Car wantedCar = this.Context.Cars.Find(id);
            IEnumerable<Part> carParts = wantedCar.Parts;

            CarVm wantedCarVm = Mapper.Map<Car, CarVm>(wantedCar);
            IEnumerable<PartVm> carPartsVms = Mapper.Map<IEnumerable<Part>, IEnumerable<PartVm>>(carParts);

            return new AboutCarVm()
            {
                Car = wantedCarVm,
                Parts = carPartsVms
            };
        }
    }
}