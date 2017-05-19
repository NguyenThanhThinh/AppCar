using AppCar.Entities.EntityModel;
using AppCar.Entities.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppCar.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterMaps();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
            });
        }
    }
}
