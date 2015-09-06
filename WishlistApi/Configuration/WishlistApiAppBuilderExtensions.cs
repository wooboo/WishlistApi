using System;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WishlistApi.Configuration;
using WishlistApi.Endpoints;

// ReSharper disable once CheckNamespace
namespace Owin
{
    public static class WishlistApiAppBuilderExtensions
    {
        public static IAppBuilder UseWishlistApi(this IAppBuilder app, WishlistApiOptions options)
        {
            app.UseWebApi(WebApiConfig.Configure(options));
            return app;
        }
    }
    public class WebApiConfig
    {
        public static HttpConfiguration Configure(WishlistApiOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var config = new HttpConfiguration();
            options.Configure?.Invoke(config);
            config.MapHttpAttributeRoutes(new InheritedDirectRouteProvider());
            //config.Routes.MapHttpRoute(
            //     name: "DefaultApi",
            //     routeTemplate: "api/{controller}/{id}",
            //     defaults: new { id = RouteParameter.Optional }
            //);
            //config.MapRestControllerRouting(typeof(MyListController));
            //config.MapRestControllerRouting(typeof(MyListWishController));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            return config;
        }
    }

    // Custom direct route provider which looks for route attributes of type 
    // InheritedRouteAttribute and also supports attribute route inheritance.
}
