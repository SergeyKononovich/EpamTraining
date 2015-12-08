using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using WebInterface.Models;

namespace WebInterface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger("WebInterface");


        protected void Application_Start()
        {
            Log.Trace("WebInterface setup started.");
            try
            {
                Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer());

                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
            catch (Exception e)
            {
                Log.Trace(e);
                throw;
            }
            Log.Trace("WebInterface start.");
        }
    }
}
