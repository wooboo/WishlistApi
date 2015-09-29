using System;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WishlistApi.Infrastructure
{
    public static class ResourceUriHelper
    {
        public static Uri GetResourceUri<TController>(this UrlHelper urlHelper, object routeValues)
        {
            var name = typeof (TController).Name.Replace("Controller", "");
            return new Uri(urlHelper.Link(name + "Api", routeValues));
        }
    }
}