using System.Collections.Generic;
using System.IdentityModel.Tokens;
using IdentityServer3.AccessTokenValidation;
using Owin;
using WishlistApi.Configuration;

namespace WebApplication4
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
                    Authority = "https://localhost:44344",
                    RequiredScopes = new[] { "write" }
                });
                inner.UseWishlistApi(new WishlistApiOptions());
            });
        }
    }
}