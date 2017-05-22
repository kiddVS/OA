﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "test1",
            //    url:"Login/{*values}",
            //    defaults:new {Controller="Login" }
            //);
            // routes.MapRoute(
            //    name: "Test",
            //    url: "{year}/{month}/{day}",
            //    defaults: new { controller = "Test", action = "Index", id = UrlParameter.Optional }
            //);
        }
    } }
