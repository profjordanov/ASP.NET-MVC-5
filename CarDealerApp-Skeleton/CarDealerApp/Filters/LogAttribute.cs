using System;
using System.IO;
using System.Web.Mvc;
using CarDealerApp.Security;

namespace CarDealerApp.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var logTimeStamp = DateTime.Now;
            var ipAddress = filterContext.HttpContext.Request.UserHostAddress;
            // var user = filterContext.HttpContext.User.Identity.Name;
            // if (string.IsNullOrEmpty(user))
            // {
            //     user = "";
            // }
            var username = "Anonymous";
            var requestCookie = filterContext.HttpContext.Request.Cookies["sessionId"];
            if (requestCookie != null)
            {
                string sessionId = requestCookie.Value;
                if (AuthenticationManager.IsAuthenticated(sessionId))
                {
                    username = AuthenticationManager.GetAuthenticatedUser(sessionId).Username;
                }
            }
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var exception = filterContext.Exception;
            var logMessage = "";
            if (exception == null)
            {
                logMessage = $"{logTimeStamp} – {ipAddress} – {username} – {controllerName}.{actionName}{Environment.NewLine}";
            }
            else
            {
                logMessage = $"[!] {logTimeStamp} – {ipAddress} – {username} – {controllerName}.{actionName} - {exception.GetType().Name} - {exception.Message}{Environment.NewLine}";
            }

            File.AppendAllText(@"D:\Test\log.txt", logMessage);
        }
    }
}