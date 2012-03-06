using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using NHLStackOverflow.Models;

namespace NHLStackOverflow
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "home", action = "index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            Database.SetInitializer(new NHLdbInitializer());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}


public class LowercaseRoute : Route
{
    public LowercaseRoute(string url, IRouteHandler routeHandler)
        : base(url, routeHandler) { }
    public LowercaseRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
        : base(url, defaults, routeHandler) { }
    public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
        : base(url, defaults, constraints, routeHandler) { }
    public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
        : base(url, defaults, constraints, dataTokens, routeHandler) { }

    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
    {
        VirtualPathData path = base.GetVirtualPath(requestContext, values);

        if (path != null)
            path.VirtualPath = path.VirtualPath.ToLowerInvariant();

        return path;
    }
}