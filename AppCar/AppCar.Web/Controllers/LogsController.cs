using System.Web.Mvc;
using AppCar.Web.Security;
using AppCar.Business;
using AppCar.Entities.ViewModels.Logs;

namespace AppCar.Web.Controllers
{
    [RoutePrefix("logs")]
    public class LogsController : Controller
    {
        private LogsBusiness business;

        public LogsController()
        {
            this.business = new LogsBusiness();
        }

        [HttpGet]
        [Route("all/{username?}")]
        public ActionResult All(string username, int? page)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Suppliers");
            }

            AllLogsPageVm vm = this.business.GetAllLogsPageVm(username, page);
            return this.View(vm);
        }

        [HttpGet]
        [Route("deleteAll")]    
        public ActionResult DeleteAll() => this.View();

        [HttpPost]
        [Route("deleteAll")]
        public ActionResult DeleteAlll()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Suppliers");
            }

            this.business.DeleteAllLogs();
            return this.RedirectToAction("All", "Suppliers");
        }

    }
}
