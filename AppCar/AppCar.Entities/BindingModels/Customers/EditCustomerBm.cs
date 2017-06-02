using System;

namespace AppCar.Entities.BindingModels.Customers
{
    public class EditCustomerBm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
