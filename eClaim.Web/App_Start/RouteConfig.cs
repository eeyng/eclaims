using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eClaim.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes(); //Enables Attribute Based Routing  


            routes.MapRoute(
  name: "login",
  url: "login",
  defaults: new { controller = "Home", action = "Login", ID = UrlParameter.Optional }
);

            routes.MapRoute(
name: "claimlisting",
url: "claimlisting",
defaults: new { controller = "Claims", action = "ClaimListing", ID = UrlParameter.Optional }
);

            routes.MapRoute(
name: "claim",
url: "claim",
defaults: new { controller = "Claims", action = "ClaimForm", ID = UrlParameter.Optional }
);


            //  routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            //);
        }
    }
}
