using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AppCar.Entities.ViewModels.Cars;
using AppCar.Entities.EntityModels;
using AppCar.Entities.ViewModels.Parts;
using AppCar.Entities.BindingModels.Cars;

namespace AppCar.Business
{
    public class CarsBusiness : Business
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

        public void AddCar(AddCarBm bind)
        {
            Car model = Mapper.Map<AddCarBm, Car>(bind);
            int[] partIds = bind.Parts.Split(' ').Select(int.Parse).ToArray();
            foreach (var partId in partIds)
            {
                Part part = this.Context.Parts.Find(partId);
                if (part != null)
                {
                    model.Parts.Add(part);
                }
            }
            this.Context.Cars.Add(model);
            this.Context.SaveChanges();
        }
    }
}
