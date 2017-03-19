using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDealerApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //  name: "Detailed sale",
            //  url: "sales/discounted/{percent}/",
            //  defaults: new { controller = "Sales", action = "Discounted", percent = UrlParameter.Optional }
            // );

          

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );


            // routes.MapRoute(
            //     name: "All customers ordered", 
            //     url: "customers/all/{order}",
            //     defaults: new {controller = "Customers", action = "All", order = "ascending"},
            //     constraints: new {order = @"ascending|descending"}
            // );

            // routes.MapRoute(
            //     name: "Cars from make",
            //     url: "car/{make}",
            //     defaults: new { controller = "Car", action = "All" }
            //     );

            //  routes.MapRoute(
            // name: "Suppliers filtered",
            // url: "suppliers/{type}",
            // defaults: new { controller = "Suppliers", action = "All",  }
            // );

            // routes.MapRoute(
            //     name: "Car with parts",
            //     url: "car/{id}/parts",
            //     defaults: new {controller = "Car", action="About"},
            //     constraints: new {id = @"\d+"}
            // );

            // routes.MapRoute(
            //    name: "Customers details",
            //    url: "customers/{id}",
            //    defaults: new { controller = "Customers", action = "About" },
            //    constraints: new { id = @"\d+" }
            //);

            // routes.MapRoute(
            //    name: "Detailed sale",
            //    url: "sales/{id}",
            //    defaults: new { controller = "Sales", action = "About" }
            //);


        }
    }
}
