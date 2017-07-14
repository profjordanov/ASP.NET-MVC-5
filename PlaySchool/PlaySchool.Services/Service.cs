using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Data;

namespace PlaySchool.Services
{
    public abstract class Service
    {
        protected PlaySchoolContext Context;

        protected Service(PlaySchoolContext context)
        {
            this.Context = context;
        }
    }
}
