using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlaySchool.Models.Attributes
{
    public class AllowAnonymousOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
            // make sure the user is not authenticated. If it's not, return true. Otherwise, return false
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            //filterContext.Result = new ViewResult() {ViewName = "~/Views/Home/Index.cshtml"};
            filterContext.HttpContext.Response.Redirect("~");
            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
