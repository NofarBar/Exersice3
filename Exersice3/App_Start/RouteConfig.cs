﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Exersice3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{action}/{ip}/{port}",
                defaults: new { controller = "Products", action = "Index", ip = UrlParameter.Optional, port = UrlParameter.Optional }
            );
   
    
        }
    }
}
