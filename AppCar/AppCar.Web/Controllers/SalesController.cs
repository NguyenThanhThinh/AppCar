using System.Web.Mvc;
using System.Collections.Generic;
using AppCar.Entities.ViewModels.Sales;
using AppCar.Web.Security;
using AppCar.Business;
using AppCar.Entities.BindingModels.Sales;

namespace AppCar.Web.Controllers
{
    [RoutePrefix("sales")]
    public class SalesController : Controller
    {
        private SalesBusiness business;

        public SalesController()
        {
            this.business = new SalesBusiness();
        }

        [HttpGet]
        [Route]
        public ActionResult All()
        {
            IEnumerable<SaleVm> vms = this.business.GetAllSales();
            return this.View(vms);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult About(int id)
        {
            SaleVm saleVm = this.business.GetSale(id);

            return this.View(saleVm);
        }

        [HttpGet]
        [Route("discounted/{percent?}/")]
        public ActionResult Discounted(double? percent)
        {
            IEnumerable<SaleVm> sales = this.business.GetDiscountedSales(percent);
            return this.View(sales);
        }

        [HttpGet]
        [Route("add/")]
        public ActionResult Add()
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            AddSaleVm vm = this.business.GetSalesVm();
            return this.View(vm);
        }

        [HttpPost]
        [Route("add/")]
        public ActionResult Add([Bind(Include = "CustomerId, CarId, Discount")] AddSaleBm bind)
        {
            if (this.ModelState.IsValid)
            {
                AddSaleConfirmationVm confirmationVm = this.business.GetSaleCofirmationVm(bind);
                return this.RedirectToAction("AddConfirmation", confirmationVm);
            }

            AddSaleVm addSaleVm = this.business.GetSalesVm();
            return this.View(addSaleVm);
        }

        [HttpGet]
        [Route("AddConfirmation")]
        public ActionResult AddConfirmation(AddSaleConfirmationVm vm)
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View(vm);
        }

        [HttpPost]
        [Route("AddConfirmation")]
        public ActionResult AddConfirmation(AddSaleBm bind)
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.business.AddSale(bind);
            return this.RedirectToAction("All");
        }



    }
}
