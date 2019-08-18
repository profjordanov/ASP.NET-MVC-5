using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Web.Configuration;

namespace ODataServer.Modules
{
    public class ConfigurationFileMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection users = new MembershipUserCollection();
            var filteredUsers = GetAllUsers(pageIndex, pageSize, out totalRecords).AsQueryable().OfType<MembershipUser>().Where((u) => String.Compare(usernameToMatch, u.UserName, true) == 0).ToList();

            filteredUsers.ForEach((u) => users.Add(u));

            return users;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;

            var config = GetFormsAuthConfiguration();

            MembershipUserCollection users = new MembershipUserCollection();

            foreach (FormsAuthenticationUser item in config.Credentials.Users)
            {
                users.Add(new MembershipUser("ConfigurationFileMembershipProvider",
                    item.Name,
                    item.Name,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    true,
                    false,
                    DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                    DateTime.Now,
                    DateTime.MinValue,
                    DateTime.MinValue,
                    DateTime.MinValue));

                totalRecords++;
            }
            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            return 0;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            int totalRecords;

            var users = FindUsersByName(providerUserKey.ToString(), 1, 1, out totalRecords);
            if (totalRecords > 0)
                return users[providerUserKey.ToString()];

            return null;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 3; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 60; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Clear; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            System.Web.Configuration.FormsAuthenticationConfiguration formsConfig = GetFormsAuthConfiguration();

            var user = formsConfig.Credentials.Users[username];
            if (user == null)
                return false;

            if (user.Password == password)
                return true;

            return false;
        }

        private static System.Web.Configuration.FormsAuthenticationConfiguration GetFormsAuthConfiguration()
        {
            var webConfig = WebConfigurationManager.OpenWebConfiguration("/web.config", "ODataServer");

            AuthenticationSection authConfig = webConfig.GetSection("system.web/authentication") as AuthenticationSection;
            System.Web.Configuration.FormsAuthenticationConfiguration formsConfig = authConfig.Forms;
            return formsConfig;
        }
    }
}