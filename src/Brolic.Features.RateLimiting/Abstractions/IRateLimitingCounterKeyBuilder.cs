using System.Threading.Tasks;

namespace Brolic.Features.RateLimiting.Abstractions
{
    public interface IRateLimitingCounterKeyBuilder
    {
        Task<string> BuildCounterKey(RateLimitingValidationContext rateLimitingValidationContext);
    }
}