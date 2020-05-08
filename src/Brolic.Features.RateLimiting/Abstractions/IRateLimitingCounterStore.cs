using System;
using System.Threading.Tasks;

namespace Brolic.Features.RateLimiting.Abstractions
{
    public interface IRateLimitingCounterStore
    {
        Task<RateLimitingCounter> GetCounter(string counterKey);
        Task SetCounter(string counterKey, RateLimitingCounter rateLimitingCounter, TimeSpan expirationTime);

    }
}