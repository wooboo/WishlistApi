using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;

namespace Owin
{
    public static class ContainerConfigAppBuilderExtension
    {
        public static HttpConfiguration ConfigureContainer(this IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // STANDARD WEB API SETUP:

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());


            // Run other optional steps, like registering filters,
            // per-controller-type services, etc., then set the dependency resolver
            // to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            // OWIN WEB API SETUP:

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            return config;
        }
    }
}
