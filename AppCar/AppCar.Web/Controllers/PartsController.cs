using System.Collections.Generic;
using System.Web.Mvc;
using AppCar.Business;
using AppCar.Entities.BindingModels.Parts;
using AppCar.Entities.ViewModels.Parts;

namespace AppCar.Web.Controllers
{
    [RoutePrefix("parts")]
    public class PartsController : Controller
    {
        private PartsBusiness business;

        public PartsController()
        {
            this.business = new PartsBusiness();
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            var vms = this.business.GetAddVm();
            return this.View(vms);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Name, Price, Quantity, SupplierId")] AddPartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.business.AddPart(bind);
                return this.RedirectToAction("All", "Cars");
            }

            var vms = this.business.GetAddVm();
            return this.View(vms);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult All()
        {
            IEnumerable<AllPartVm> vms = this.business.GetAllPartVms();
            return this.View(vms);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            DeletePartVm vm = this.business.GetDeleteVm(id);
            return this.View(vm);
        }

        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete([Bind(Include = "PartId")] DeletePartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.business.DeletePart(bind);
                return this.RedirectToAction("All");
            }

            DeletePartVm vm = this.business.GetDeleteVm(bind.PartId);
            return this.View(vm);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            EditPartVm vm = this.business.GetEditVm(id);
            return this.View(vm);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Price, Quantity")] EditPartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.business.EditPart(bind);
                return this.RedirectToAction("All");
            }

            EditPartVm vm = this.business.GetEditVm(bind.Id);
            return this.View(vm);
        }
    }
}
