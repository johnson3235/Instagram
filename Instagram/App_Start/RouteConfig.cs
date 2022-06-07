﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Instagram
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "Post/Index",
                defaults: new { controller = "Post", action = "Index" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login/Index",
                defaults: new { controller = "Login", action = "Index" }
            );

      



        }
    }
}