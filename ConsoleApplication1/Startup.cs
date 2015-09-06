using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;
using WishlistApi.Configuration;
using Swashbuckle.Swagger;

namespace ConsoleApplication1
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();



            appBuilder.Map("/wishlist", inner =>
            {
                inner.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    TokenProvider = new QueryStringOAuthBearerProvider("api_key"),
                    Authority = "https://localhost:44344",
                    RequiredScopes = new[] { "write" }
                });
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
    public class QueryStringOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        readonly string _name;

        public QueryStringOAuthBearerProvider(string name)
        {
            _name = name;
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var value = context.Request.Query.Get(_name);

            if (!string.IsNullOrEmpty(value))
            {
                
                context.Token = value.Split(' ')[1];
            }

            return Task.FromResult<object>(null);
        }
    }
}