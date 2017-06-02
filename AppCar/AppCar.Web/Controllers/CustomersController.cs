using System.Collections.Generic;
using System.Web.Mvc;
using AppCar.Business;
using AppCar.Entities.ViewModels.Customers;
using AppCar.Entities.BindingModels.Customers;

namespace AppCar.Web.Controllers
{
    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        private CustomersBusiness business;

        public CustomersController()
        {
            this.business = new CustomersBusiness();
        }

        [HttpGet]
        [Route("all/{order:regex(ascending|descending)}")]
        public ActionResult All(string order)
        {
            IEnumerable<AllCustomerVm> viewModels = this.business.GetAllOrderedCustomers(order);
            return this.View(viewModels);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult About(int id)
        {
            AboutCustomerVm vm = this.business.GetCustomerWithCarData(id);

            return this.View(vm);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Name,BirthDate")] AddCustomerBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.business.AddCustomerBm(bind);

                return this.RedirectToAction("All", new { order = "ascending" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            EditCustomerVm vm = this.business.GetEditVm(id);

            return this.View(vm);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate")] EditCustomerBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.business.EditCustomer(bind);
                return this.RedirectToAction("All", new { order = "ascending" });
            }

            return this.View(this.business.GetEditVm(bind.Id));
        }
    }
}