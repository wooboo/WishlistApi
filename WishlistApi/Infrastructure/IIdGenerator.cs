using System;
using WishlistApi.Model.Domain;

namespace WishlistApi.Infrastructure
{
    public interface IIdGenerator<T>
        where T: IIdentified
    {
        string GenerateId(T entity);
    }

    public class TypedIdGenerator<T> : IIdGenerator<T> 
        where T : IIdentified
    {
        private readonly RadixEncoding _radixEncoding;

        public TypedIdGenerator(RadixEncoding radixEncoding)
        {
            _radixEncoding = radixEncoding;
        }

        public virtual string GenerateId(T entity)
        {
            var encode = _radixEncoding.Encode(Guid.NewGuid().ToByteArray());
            var typeHashCode = typeof (T).Name.GetHashCode();
            var t = _radixEncoding.Encode(BitConverter.GetBytes(typeHashCode));

            var id= $"{encode}_{t}";
            entity.Id = id;
            return id;
        }
    }
}