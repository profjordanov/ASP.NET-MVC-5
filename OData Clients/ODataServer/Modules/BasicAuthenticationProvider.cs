using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Security.Principal;
using System.Web.Security;
using System.Text;

namespace ODataServer.Modules
{
    public class BasicAuthenticationProvider
    {

        internal static bool Authenticate(HttpContext httpContext)
        {
            //ensure SSL
            if (!httpContext.Request.IsSecureConnection)
            { 
                return false;
            }

            //ensure header is present
            if(!httpContext.Request.Headers.AllKeys.Contains(HttpRequestHeader.Authorization.ToString()))
            {
                return false;
            }

            //ensure header has value
            var requestHeader = httpContext.Request.Headers[HttpRequestHeader.Authorization.ToString()];

            if(string.IsNullOrEmpty(requestHeader) || 
                !requestHeader.StartsWith(AuthenticationSchemes.Basic.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            var credentials = GetCredentialsFromHeader(requestHeader);
            if(credentials == null)
                return false;

            //try to get the principal and set it on the thread
            IPrincipal user = null;
            var authenticated = TryGetPrincipal(credentials, out user);
            if(authenticated)
                httpContext.User = user;

            //return indicating if we could authenticate
            return authenticated;
        }

        private static bool TryGetPrincipal(string[] credentials, out IPrincipal user)
        {
            if (FormsAuthentication.Authenticate(credentials[0], credentials[1]))
            {
                user = new GenericPrincipal(
                    new GenericIdentity(credentials[0], AuthenticationSchemes.Basic.ToString()),
                    new string[] { "User" });
                return true;
            }

            user = null;
            return false;
        }

        private static string[] GetCredentialsFromHeader(string requestHeader)
        {
            string base64Credentials = requestHeader.Substring(6);
            string[] credentials = Encoding.ASCII.GetString(
                  Convert.FromBase64String(base64Credentials)
            ).Split(new char[] { ':' });

            if (credentials.Length != 2 ||
                string.IsNullOrEmpty(credentials[0]) ||
                string.IsNullOrEmpty(credentials[1])
            ) return null;

            // Okay this is the credentials 
            return credentials; 


        }
    }
}