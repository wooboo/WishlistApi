using System;
using WishlistApi.Model.Domain;

namespace WishlistApi.Infrastructure
{
    public class SlugIdGenerator<T> : IIdGenerator<T> 
        where T : IIdentified
    {
        private readonly SlugGenerator _slugGenerator;
        private readonly Func<T, string> _nameGetter;

        public SlugIdGenerator(SlugGenerator slugGenerator, Func<T, string> nameGetter)
        {
            _slugGenerator = slugGenerator;
            _nameGetter = nameGetter;
        }

        public string GenerateId(T entity)
        {
            var id = _slugGenerator.ToUrlSlug(_nameGetter(entity));
            return id;
        }
    }
}