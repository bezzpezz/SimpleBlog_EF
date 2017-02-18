using SimpleBlog_EF.Controllers.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleBlog_EF
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var namespaces = new[] { typeof(PostsController).Namespace };

            //routes.MapRoute("Tag", "tag/{id}-{slug}", new { Controller = "Home", action = "Tag"}, namespaces);

            //routes.MapRoute("PostForRealTHisTime", "post/{idAndslug}", new { Controller = "Posts", action = "Show" }, namespaces);
            //routes.MapRoute("Post", "post/{id}-{slug}", new { Controller = "Home", action = "Show" }, namespaces);
            routes.MapRoute(
                name: "TagForRealThisTime",
                url: "tag/{idAndSlug}",
                defaults: new { controller = "Posts", action = "Tag"},
                namespaces: namespaces
                );
            routes.MapRoute(
                name: "Tag",
                url: "tag/{id}-{slug}",
                defaults: new { controller = "Posts", action = "Tag" },
                namespaces: namespaces
            );

            routes.MapRoute(
                name: "PostForRealThisTime",
                url: "post/{idAndSlug}",
                defaults: new { controller = "Posts", action = "Show" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Post",
                url: "post/{id}-{slug}",
                defaults: new { controller = "Posts", action = "Show" },
                namespaces: namespaces
            );

            // Login
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Auth", action = "Login"}
            );

            //Logout Route
            routes.MapRoute(
                name: "Logout",
                url: "logout",
                defaults: new { controller = "Auth", action = "Logout" },
                namespaces: namespaces
            );

            // Home
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Posts", action = "Index" },
                namespaces: namespaces
            );

            // Orginal route
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
