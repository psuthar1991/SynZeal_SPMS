using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Web.Optimization;
using System.Web.Http;

namespace Synzeal_Inventory
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IDisposable _sentry;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        
        {
            if(HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Timeout = 60;
            }
        }
    }
}