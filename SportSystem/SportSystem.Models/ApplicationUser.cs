namespace SportSystem.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        private ICollection<Comment> comments;
        private ICollection<UserMatchBet> userMatchesBetses;

        public ApplicationUser()
        {
            this.comments = new HashSet<Comment>();
            this.userMatchesBetses = new HashSet<UserMatchBet>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<UserMatchBet> UserMatchesBetses
        {
            get { return this.userMatchesBetses; }
            set { this.userMatchesBetses = value; }
        }
    }
}
