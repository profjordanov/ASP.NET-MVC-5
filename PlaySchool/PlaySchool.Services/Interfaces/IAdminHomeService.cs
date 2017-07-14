using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Models.BMs.Admin;

namespace PlaySchool.Services.Interfaces
{
    public interface IAdminHomeService
    {
        string PromoteUserToAdmin(PromoteUserBm username);
    }
}
