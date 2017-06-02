using System.Collections.Generic;
using AppCar.Entities.ViewModels.Parts;

namespace AppCar.Entities.ViewModels.Cars
{
    public class AboutCarVm
    {
        public CarVm Car { get; set; }

        public IEnumerable<PartVm> Parts { get; set; }
    }
}
