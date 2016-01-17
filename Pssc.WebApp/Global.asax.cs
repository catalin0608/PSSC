using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Pssc.Database;
using System.Reflection;

namespace Pssc.WebApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<PsscEntities>().AsSelf().InstancePerRequest();
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(containerBuilder.Build()));
        }
    }
}