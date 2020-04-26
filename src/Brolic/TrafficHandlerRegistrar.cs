using System;
using Brolic.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Brolic
{
    public class TrafficHandlerRegistrar: ITrafficHandlerRegistrar
    {
        private readonly string _keyPrefix = nameof(TrafficHandlerRegistrar);
        private readonly IMemoryCache _memoryCache;

        public TrafficHandlerRegistrar(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void RegisterTrafficHandler<TTrafficHandler>(string key) 
            where TTrafficHandler : ITrafficHandler
        {
            _memoryCache.Set($"{_keyPrefix}/{key}", typeof(TTrafficHandler));
        }

        public Type ResolveTrafficHandler(string key)
        {
            return _memoryCache.Get<Type>($"{_keyPrefix}/{key}");
        }
    }
}