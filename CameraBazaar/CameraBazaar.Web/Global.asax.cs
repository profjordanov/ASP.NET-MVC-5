using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CameraBazaar.Models.BindingModels;
using CameraBazaar.Models.Enitities;
using AutoMapper;
using CameraBazaar.Models.ViewModels;

namespace CameraBazaar.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.ConfigureMapper();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<RegisterUserBm, User>();
                expression.CreateMap<Camera, ShortCameraVm>();
                expression.CreateMap<Camera, DetailsCameraVm>();
            });
        }
    }
}
