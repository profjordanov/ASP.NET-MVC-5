using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Models.BMs.Account;
using PlaySchool.Models.VMs.Account;

namespace PlaySchool.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterPlayer(RegisterBm bind);
        ProfileViewModel GetProfile(string username);
    }
}
