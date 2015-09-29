using System;
using System.Web.Http;
using WishlistApi.Configuration;

namespace Owin
{
    public class WebApiConfig
    {
        public static HttpConfiguration Configure(HttpConfiguration config, WishlistApiOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

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
}