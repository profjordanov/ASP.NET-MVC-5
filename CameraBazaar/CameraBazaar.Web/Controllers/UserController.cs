using System.Web;
using System.Web.Mvc;
using CameraBazaar.Models.BindingModels;
using CameraBazaar.Models.ViewModels;
using CameraBazaar.Services;
using CameraBazaar.Web.Security;

namespace CameraBazaar.Web.Controllers
{
    [RoutePrefix("users")]
    public class UserController : Controller
    {
        private UsersService service;

        public UserController()
        {
            this.service = new UsersService();
        }

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            return this.View(new RegisterUserVm());
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(RegisterUserBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.RegisterUser(bind);
                return this.RedirectToAction("Login");
            }

            return this.View(new RegisterUserVm());
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return this.View(new LoginUserVm());
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginUserBm bind)
        {
            if (this.ModelState.IsValid && this.service.UserExists(bind))
            {
                this.service.LoginUser(bind, this.Session.SessionID);
                this.Response.Cookies.Add(new HttpCookie("sessionId", this.Session.SessionID));
                return this.RedirectToAction("Profile");
            }

            return this.View(new LoginUserVm());
        }

        [HttpPost]
        [Route("logout")]
        public ActionResult Logout()
        {
            string sessionId = this.Request.Cookies.Get("sessionId")?.Value;
            if (AuthenticationManager.IsAuthenticated(sessionId))
            {
                AuthenticationManager.Logout(sessionId);
            }

            return this.RedirectToAction("Login", "User");
        }
    }
}