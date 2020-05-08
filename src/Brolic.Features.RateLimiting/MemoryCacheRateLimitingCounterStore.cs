using System;
using System.Threading.Tasks;
using Brolic.Features.RateLimiting.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Brolic.Features.RateLimiting
{
    public class MemoryCacheRateLimitingCounterStore: IRateLimitingCounterStore
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheRateLimitingCounterStore(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        

        public Task<RateLimitingCounter> GetCounter(string counterKey)
        {
            var rateLimitingCounter =  _memoryCache.Get<RateLimitingCounter>(counterKey);
            return Task.FromResult(rateLimitingCounter);
        }
        
        public Task SetCounter(string counterKey, RateLimitingCounter rateLimitingCounter, TimeSpan expirationTime)
        {
            _memoryCache.Set(counterKey, rateLimitingCounter, expirationTime);
            return Task.CompletedTask;
        }
        
    }
}