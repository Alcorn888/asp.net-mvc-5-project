using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Tickets
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // подрубим наши binders
            var binder = new Tickets.Models.Core.DateTimeModelBinder();
            ModelBinders.Binders.Add(typeof(DateTime), binder);
            ModelBinders.Binders.Add(typeof(DateTime?), binder);
        }
    }
}
