using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Entities.ViewModel
{
    public class AboutCustomerVm
    {
        public string Name { get; set; }

        public int BoughtCarsCount { get; set; }

        public double? TotalSpentMoney { get; set; }
    }
}
