using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PlaySchool.Data;
using PlaySchool.Models.BMs.Admin;
using PlaySchool.Models.EntityModels;
using PlaySchool.Services.Interfaces;

namespace PlaySchool.Services
{
    public class AdminHomeService : Service,IAdminHomeService
    {
        public AdminHomeService(PlaySchoolContext context) : base(context)
        {
        }

        public string PromoteUserToAdmin(PromoteUserBm bind)
        {
            ApplicationUser appUser = Context.Users.FirstOrDefault(u=>u.UserName==bind.Username);
            if (appUser == null)
            {
                return $"User with username:{bind.Username} doesn't exist!";
            }
            if (appUser.Roles.Any(r => r.RoleId == Context.Roles.FirstOrDefault(ro => ro.Name == "Admin").Id))
            {
                return "This user is already admin!";
            }
            string roleId = Context.Roles.FirstOrDefault(ro => ro.Name == "Admin").Id;
            Context.Users.Find(appUser.Id).Roles.Add(new IdentityUserRole() {UserId = appUser.Id,RoleId = roleId});
            Context.SaveChanges();
            return "Success!";
        }
    }
}
