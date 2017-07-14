using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Data;
using PlaySchool.Models;
using PlaySchool.Models.BMs.Account;
using PlaySchool.Models.EntityModels;
using PlaySchool.Models.VMs.Account;
using PlaySchool.Services.Interfaces;

namespace PlaySchool.Services
{
    public class AccountService : Service, IAccountService
    {
        public AccountService(PlaySchoolContext context) : base(context)
        {
        }

        public void RegisterPlayer(RegisterBm bind)
        {
            if (string.IsNullOrEmpty(bind.FirstName) || string.IsNullOrEmpty(bind.LastName))
            {
                bind.FirstName = "Default";
                bind.LastName = "Name";
            }
            Context.Players.Add(new Player()
            {
                AppUser = Context.Users.FirstOrDefault(u => u.Email == bind.Email),
                Points = 5
            });
            Context.SaveChanges();
        }

        public ProfileViewModel GetProfile(string username)
        {
            var player = Context.Players.FirstOrDefault(p => p.AppUser.UserName == username);
            if (player == null)
            {
                return null;
            }
            ProfileViewModel pvm = AutoMapper.Mapper.Map<ProfileViewModel>(player);
            if (player.HaveProfilePicture)
            {
                pvm.ProfilePictureName = Constants.ProfilePicturePrefix + pvm.ProfilePictureName;
            }
            else
            {
                pvm.ProfilePictureName = Constants.ProfilePicturePrefix + Constants.DefaultProfilePicture;
            }
            pvm.Username = player.AppUser.UserName;
            pvm.Email = player.AppUser.Email;
            return pvm;
        }
    }
}
