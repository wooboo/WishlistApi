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
            var config = app.ConfigureContainer(new HttpConfiguration());

            app.UseWebApi(WebApiConfig.Configure(config, options));
            return app;
        }
    }

    // Custom direct route provider which looks for route attributes of type 
    // InheritedRouteAttribute and also supports attribute route inheritance.
}
