using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net.Http;
using Microsoft.Owin.Cors;
using Owin;
using Swashbuckle.Application;
using WishlistApi.Configuration;

namespace ConsoleHost
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