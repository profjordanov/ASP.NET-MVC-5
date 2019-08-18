using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ODataServer.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string pwd)
        {
            if (FormsAuthentication.Authenticate(userName, pwd))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return Redirect(Request.QueryString["returnUrl"]);
            }
            else
            {
                ModelState.AddModelError("userName", "Invalid username and password");
                return View();
            }
        }

    }
}
