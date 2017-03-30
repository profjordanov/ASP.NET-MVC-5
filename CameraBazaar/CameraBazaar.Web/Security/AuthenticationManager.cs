using System.Linq;
using CameraBazaar.Data;
using CameraBazaar.Models.Enitities;

namespace CameraBazaar.Web.Security
{
    public class AuthenticationManager
    {
        private static CameraBazaarContext context = new CameraBazaarContext();

        public static bool IsAuthenticated(string sessionId)
        {
            if (sessionId == null)
            {
                return false;
            }

            if (context.Logins.Any(login => login.SessionId == sessionId && login.IsActive))
            {
                return true;
            }

            return false;
        }

        public static void Logout(string sessionId)
        {
            Login login = context.Logins.FirstOrDefault(login1 => login1.SessionId == sessionId);
            login.IsActive = false;
            context.SaveChanges();
        }

        public static User GetAuthenticatedUser(string sessionId)
        {
            var firstOrDefault = context.Logins.FirstOrDefault(login => login.SessionId == sessionId
                                                                        && login.IsActive);
            if (firstOrDefault != null)
            {
                var authenticatedUser = firstOrDefault.User;
                if (authenticatedUser != null)
                {
                    return authenticatedUser;
                }
            }

            return null;
        }
    }
}