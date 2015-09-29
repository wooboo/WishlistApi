using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using WishlistApi.Infrastructure;
using WishlistApi.Model.Domain;
using Module = Autofac.Module;

namespace WishlistApi
{
    public class ApiModule : Module
    {
        /// <summary>
        /// Add registrations to the container.
        /// </summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        ///             registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FakeUserIdProvider>().As<IUserIdProvider>();
            builder.RegisterType<DTOMapper>();
            builder.RegisterType<SlugGenerator>();
            builder.RegisterType<TypedIdGenerator<WishList>>().As<IIdGenerator<WishList>>();
            builder.Register(c => new SlugIdGenerator<Wish>(c.Resolve<SlugGenerator>(), w => w.Name)).As<IIdGenerator<Wish>>();
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}
