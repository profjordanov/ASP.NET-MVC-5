using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using PlaySchool.Models.BMs.Account;
using PlaySchool.Models.EntityModels;
using PlaySchool.Models.VMs.Account;
using PlaySchool.Models.VMs.Manage;
using PlaySchool.Models.VMs.Game;

namespace PlaySchool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.ConfigureMapper();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<RegisterBm, Player>();
                expression.CreateMap<RegisterBm, RegisterViewModel>();
                expression.CreateMap<Player, ProfileViewModel>();
                expression.CreateMap<ChangeNameBm, ChangeNameViewModel>();
                expression.CreateMap<Game, ShowGameViewModel>();
                expression.CreateMap<Game, SmallGameViewModel>();
            });
        }
    }
}
