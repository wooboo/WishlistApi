using System;
using AutoMapper;

namespace WishlistApi.Infrastructure
{
    public class DTOMapper
    {
        private readonly IMappingEngine _mapper;

        public DTOMapper()
        {
            _mapper = Mapper.Engine;
        }

        public MapTo<TSource> Map<TSource>(TSource source)
        {
            return new MapTo<TSource>(_mapper, source);
        }
        public class MapTo<TSource>
        {
            private readonly IMappingEngine _mapper;
            private readonly TSource _source;

            public MapTo(IMappingEngine mapper, TSource source)
            {
                _mapper = mapper;
                _source = source;
            }

            public TDest To<TDest>(Action<TSource, TDest> customs = null)
            {
                var dest = _mapper.DynamicMap<TSource, TDest>(_source);
                customs?.Invoke(_source, dest);
                return dest;
            }

            public TDest To<TDest>(TDest dest, Action<TSource, TDest> customs = null)
            {
                _mapper.DynamicMap<TSource, TDest>(_source, dest);
                customs?.Invoke(_source, dest);
                return dest;
            }
        }
    }
}