using System.Web;
using System.Web.Mvc;
using AppCar.Web.Security;
using AppCar.Business;
using AppCar.Entities.BindingModels.Users;

namespace AppCar.Web.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private UsersBusiness business;

        public UsersController()
        {
            this.business = new UsersBusiness();
        }

        [HttpGet]
        [Route("register/")]
        public ActionResult Register()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null && AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            return this.View();
        }

        [HttpPost]
        [Route("register/")]
        public ActionResult Register([Bind(Include = "Email, Username, Password, ConfirmPassword")]RegisterUserBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null && AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            if (this.ModelState.IsValid && bind.ConfirmPassword == bind.Password)
            {
                this.business.RegisterUser(bind);
                return this.RedirectToAction("Login");
            }

            return this.RedirectToAction("Register");
        }

        [HttpGet]
        [Route("login/")]
        public ActionResult Login()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null && AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            return this.View();
        }

        [HttpPost]
        [Route("login/")]
        public ActionResult Login([Bind(Include = "Username, Password")]LoginUserBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null && AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            if (this.ModelState.IsValid && this.business.UserExists(bind))
            {
                this.business.LoginUser(bind, Session.SessionID);
                this.Response.SetCookie(new HttpCookie("sessionId", Session.SessionID));
                return this.RedirectToAction("All", "Cars");
            }

            return this.RedirectToAction("Login");
        }

        [HttpPost]
        [Route("logout")]
        public ActionResult Logout()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            Request.Cookies.Clear();
            Response.Cookies.Clear();
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login");
            }

            //AuthenticationManager.Logout(Request.Cookies.Get("sessionId").Value);
            return this.RedirectToAction("All", "Cars");
        }


    }
}
