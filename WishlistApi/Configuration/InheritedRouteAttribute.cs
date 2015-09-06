using System;
using System.Linq;
using System.Web.Http.Routing;

namespace Owin
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class InheritedRouteAttribute : Attribute, IDirectRouteFactory
    {
        public InheritedRouteAttribute(string template)
        {
            Template = template;
        }
        public int Order { get; set; }
        public string Template { get; private set; }
        public new RouteEntry CreateRoute(DirectRouteFactoryContext context)
        {
            // context.Actions will always contain at least one action - and all of the 
            // actions will always belong to the same controller.
            var controllerDescriptor = context.Actions.First().ControllerDescriptor;
            var template = Template.Replace("{controller}",
                controllerDescriptor.ControllerName);
            IDirectRouteBuilder builder = context.CreateBuilder(template);
            builder.Name = controllerDescriptor.ControllerName+"Api";
            builder.Order = Order;
            return builder.Build();
        }
    }
}