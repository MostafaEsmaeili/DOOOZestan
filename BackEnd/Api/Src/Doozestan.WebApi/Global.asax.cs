using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Framework.Logging;

namespace Doozestan.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private CustomLogger Logger => new CustomLogger(this.GetType().FullName);

        protected void Application_Start()
        {
            new BootStrapper().Init();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var ex = new Exception();
                var exception = Server.GetLastError();
                var msg = (((HttpApplication)(sender)).Context.Error);
                ex = msg ?? exception;
                if (!exception.Message.Contains("favicon"))
                    Logger.ErrorException("Error on MVC App in Globals : " + ex.Message, ex);
            }
            catch
            {
                // failed to record exception
            }
        }
    }
}
