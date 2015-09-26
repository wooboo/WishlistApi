using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace ConsoleHost
{
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