using System.Threading.Tasks;

namespace Brolic.Features.RateLimiting.Abstractions
{
    public interface IRateLimitingValidationStrategy
    {
        public Task<RateLimitingValidationResponse> ValidateContext(RateLimitingValidationContext rateLimitingValidationContext);
    }
}