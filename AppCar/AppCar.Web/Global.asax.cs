using AppCar.Entities.BindingModels.Cars;
using AppCar.Entities.BindingModels.Customers;
using AppCar.Entities.BindingModels.Parts;
using AppCar.Entities.BindingModels.Suppliers;
using AppCar.Entities.BindingModels.Users;
using AppCar.Entities.EntityModels;
using AppCar.Entities.ViewModels.Cars;
using AppCar.Entities.ViewModels.Customers;
using AppCar.Entities.ViewModels.Parts;
using AppCar.Entities.ViewModels.Sales;
using AppCar.Entities.ViewModels.Suppliers;
using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppCar.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
         
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            this.RegisterMaps();
        }
        private void RegisterMaps()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Customer, AllCustomerVm>();
                expression.CreateMap<Car, CarVm>();
                expression.CreateMap<Supplier, SupplierVm>()
                    .ForMember(vm => vm.NumberOfPartsToSupply,
                        configurationExpression =>
                            configurationExpression.MapFrom(supplier => supplier.Parts.Count));
                expression.CreateMap<Part, PartVm>();
                expression.CreateMap<Sale, SaleVm>()
                .ForMember(vm => vm.Price,
                    configurationExpression =>
                    configurationExpression.MapFrom(sale =>
                            sale.Car.Parts.Sum(part => part.Price)));
                expression.CreateMap<AddCustomerBm, Customer>();
                expression.CreateMap<EditCustomerBm, Customer>();
                expression.CreateMap<Customer, EditCustomerVm>();
                expression.CreateMap<EditCustomerBm, EditCustomerVm>();
                expression.CreateMap<AddPartBm, Part>();
                expression.CreateMap<Part, AllPartVm>();
                expression.CreateMap<Part, DeletePartVm>();
                expression.CreateMap<Part, EditPartVm>();
                expression.CreateMap<AddCarBm, Car>().ForMember(car => car.Parts, configurationExpression => configurationExpression.Ignore());
                expression.CreateMap<RegisterUserBm, User>();
                expression.CreateMap<Car, AddSaleCarVm>().ForMember(vm => vm.MakeAndModel, configurationExpression => configurationExpression.MapFrom(car => $"{car.Make} {car.Model}"));
                expression.CreateMap<Customer, AddSaleCustomerVm>();
                expression.CreateMap<Supplier, SupplierAllVm>();
                expression.CreateMap<AddSupplierBm, Supplier>();
                expression.CreateMap<Supplier, EditSupplierVm>();
                expression.CreateMap<Supplier, DeleteSuplierVm>();
            });
        }
    }
}
