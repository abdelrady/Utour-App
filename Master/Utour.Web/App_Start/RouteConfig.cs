using System.Web.Mvc;
using System.Web.Routing;
using System.ServiceModel.Activation;
using DistributedServices.UTourService;

namespace Utour.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.svc/{*pathInfo}");
            routes.Add(new ServiceRoute("Utour", new WebServiceHostFactory(),
                typeof(HotSpotsMgmtService)));

            //routes.MapRoute(
            //    "MyService", // Route name
            //    "Utour/{action}/{id}", // URL with parameters
            //    new
            //    {
            //        controller = "Home",
            //        action = "Index",
            //        id = UrlParameter.Optional
            //    }, // Parameter defaults
            //    new { controller = "^(?!Home).*" }
            //);
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}