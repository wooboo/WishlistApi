using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MongoDB.Driver;
using WishlistApi.Infrastructure;
using WishlistApi.Model.DataAccess;
using WishlistApi.Model.Domain;

namespace WishlistApi.Model
{
    public class DataModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MongoClient()).As<IMongoClient>();
            builder.Register(c => c.Resolve<IMongoClient>().GetDatabase("wishlist")).As<IMongoDatabase>();
            builder.Register(context => new MongoRepository<WishList>("List", context.Resolve<IMongoDatabase>()))
                .As<IRepository<WishList>>();
            builder.Register(c => new RadixEncoding("0123456789abcdef" +
                                                    "ghijklmnopqrstuv" +
                                                    "wxyz"));
        }
    }
}
