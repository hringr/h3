using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace hringr
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Group",
                url: "Group/{action}/{id}",
                defaults: new { controller = "Group", action = "Details" }
                );

            routes.MapRoute(
                name: "Posts",
                url: "Posts/{action}/{id}",
                defaults: new { controller = "Posts", action = "Details" }
                );

            routes.MapRoute(
                name: "Follow",
                url: "{controller}/{action}/{u}",
                defaults: new { controller = "Follow", action = "Add" }
            );

            routes.MapRoute(
                name: "User",
                url: "{controller}/{action}/{u}",
                defaults: new { controller = "User", action = "Details" }
            );

            routes.MapRoute(
                name: "Search",
                url: "{controller}/{action}/{q}",
                defaults: new { controller = "Search", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
