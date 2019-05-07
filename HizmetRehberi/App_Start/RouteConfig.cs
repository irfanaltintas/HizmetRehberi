using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HizmetRehberi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Firms",
                url: "Firmalar",
                defaults: new { controller = "Home", action = "Firms"}
            );

            routes.MapRoute(
                name: "About",
                url: "Hakkımızda",
                defaults: new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                name: "OneFirm",
                url: "Firma",
                defaults: new { controller = "Home", action = "OneFirm", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
