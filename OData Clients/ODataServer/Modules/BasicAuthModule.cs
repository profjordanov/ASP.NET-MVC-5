using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataServer.Modules
{
    public class BasicAuthModule : IHttpModule
    {
        

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            
            //allow odata metadata requests without authentication
            if (application.Context.Request.Url.ToString().EndsWith("$metadata", StringComparison.InvariantCultureIgnoreCase))
                return;

            //allow client access policy without authentication
            if (application.Context.Request.Url.ToString().EndsWith("clientaccesspolicy.xml", StringComparison.InvariantCultureIgnoreCase))
                return;

            if (!BasicAuthenticationProvider.Authenticate(application.Context))
            {
                application.Context.Response.Status = "401 Unauthorized";
                application.Context.Response.StatusCode = 401;
                application.Context.Response.AddHeader("WWW-Authenticate", "Basic");
                application.CompleteRequest();
            }
        } 

    }
}