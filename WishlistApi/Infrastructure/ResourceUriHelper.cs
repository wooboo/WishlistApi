using System;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WishlistApi.Infrastructure
{
    public class ResourceUriHelper
    {
        private readonly ApiController _controller;

        public ResourceUriHelper(ApiController controller)
        {
            _controller = controller;
        }
        public Uri GetResourceUri<TController>(object routeValues)
        {
            var name = typeof (TController).Name.Replace("Controller", "");
            var urlHelper = _controller.Url;
            return new Uri(urlHelper.Link(name + "Api", routeValues));
        }
    }
}