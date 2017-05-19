using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Entities.ViewModel
{
    public class AboutCarVm
    {
        public CarVm Car { get; set; }

        public IEnumerable<PartVm> Parts { get; set; }
    }
}
