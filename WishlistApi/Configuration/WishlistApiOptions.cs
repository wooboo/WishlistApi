using System;
using System.Web.Http;

namespace WishlistApi.Configuration
{
    public class WishlistApiOptions
    {
        public Action<HttpConfiguration> Configure { get; set; }
    }
}