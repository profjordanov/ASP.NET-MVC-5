using System.Linq;
using CarDealer.Data;
using CarDealer.Models;
using Microsoft.Ajax.Utilities;

namespace CarDealerApp.Security
{
    public class AuthenticationManager
    {
        private static CarDealerContext context = new CarDealerContext();

        public static bool IsAuthenticated(string sessionId)
        {
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
            var firstOrDefault = context.Logins.FirstOrDefault(login => login.SessionId == sessionId && login.IsActive);
            if (firstOrDefault != null)
            {
                var authenticatedUser = firstOrDefault.User;
                if (authenticatedUser != null)
                    return authenticatedUser;
            }
            return null;
        }
    }
}