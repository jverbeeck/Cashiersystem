using System.Web.Mvc;
using System.Web.Routing;

namespace Website
{
    /// <summary>
    /// This class handles the routing 
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// This class handles routes for the MVC Platform
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
