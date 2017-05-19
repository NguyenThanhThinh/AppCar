using AppCar.Entities.EntityModel;
using AppCar.Entities.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Business
{
   public class CarBusiness:Business
    {
        public IEnumerable<CarVm> GetCarsFromGivenMakeInOrder(string make)
        {
            IEnumerable<Car> cars;
            if (make == null)
            {
                cars = this.Context.Cars.OrderBy(car => car.Make).ThenByDescending(car => car.TravelledDistance);
            }
            else
            {
                cars = this.Context.Cars.Where(car => car.Make.Contains(make)).OrderBy(car => car.Make).ThenByDescending(car => car.TravelledDistance);
            }

            IEnumerable<CarVm> viewModels = Mapper.Instance.Map<IEnumerable<Car>, IEnumerable<CarVm>>(cars);
            return viewModels;
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
