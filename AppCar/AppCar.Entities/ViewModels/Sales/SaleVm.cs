using AppCar.Entities.ViewModels.Cars;
using AppCar.Entities.ViewModels.Customers;

namespace AppCar.Entities.ViewModels.Sales
{
    public class SaleVm
    {
        public CarVm Car { get; set; }

        public AllCustomerVm Customer { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }
    }
}
