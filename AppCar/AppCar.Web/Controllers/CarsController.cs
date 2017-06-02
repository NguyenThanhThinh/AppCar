using System.Collections.Generic;
using System.Web.Mvc;
using AppCar.Web.Security;
using AppCar.Business;
using AppCar.Entities.ViewModels.Cars;
using AppCar.Entities.BindingModels.Cars;

namespace AppCar.Web.Controllers
{
    [RoutePrefix("cars")]
    public class CarsController : Controller
    {
        private CarsBusiness busineess;

        public CarsController()
        {
            this.busineess = new CarsBusiness();
        }

        [HttpGet]
        [Route("{make?}")]
        public ActionResult All(string make)
        {
            IEnumerable<CarVm> modelCarVms = this.busineess.GetCarsFromGivenMakeInOrder(make);
            return this.View(modelCarVms);
        }

        [HttpGet]
        [Route("{id:int}/parts")]
        public ActionResult About(int id)
        {
            AboutCarVm vm = this.busineess.GetCarWithParts(id);

            return this.View(vm);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance, Parts")] AddCarBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (this.ModelState.IsValid)
            {
                this.busineess.AddCar(bind);

                return this.RedirectToAction("All");
            }

            return this.View();
        }
    }
}