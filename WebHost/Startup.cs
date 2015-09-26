using System.Collections.Generic;
using System.IdentityModel.Tokens;
using IdentityServer3.AccessTokenValidation;
using Owin;
using WishlistApi.Configuration;
using Microsoft.Owin.Cors;
using Swashbuckle.Application;
using System;
using System.Net.Http;
using System.IO;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(WebHost.Startup))]
namespace WebHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            appBuilder.UseCors(CorsOptions.AllowAll);



            appBuilder.Map("/wishlist", inner =>
            {
                //inner.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                //{
                //    TokenProvider = new QueryStringOAuthBearerProvider("api_key"),
                //    Authority = "https://localhost:44344",
                //    RequiredScopes = new[] { "write" }
                //});
                var wishlistApiOptions = new WishlistApiOptions();
                wishlistApiOptions.Configure = configuration =>
                {
                    configuration
                        .EnableSwagger(c =>
                        {
                            c.ApiKey("Authorization").In("header");

                            c.RootUrl(o => o.RequestUri.GetLeftPart(UriPartial.Authority) +
                                           o.GetRequestContext().VirtualPathRoot.TrimEnd('/'));
                            c.SingleApiVersion("v1", "A title for your API");

                            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            var commentsFileName = typeof(WishlistApiOptions).Assembly.GetName().Name + ".XML";
                            var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                            c.IncludeXmlComments(commentsFile);
                        })
                        .EnableSwaggerUi();
                };
                inner.UseWishlistApi(wishlistApiOptions);
            });
        }
    }
}