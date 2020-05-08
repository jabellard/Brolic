using System.Threading.Tasks;

namespace Brolic.Features.RateLimiting.Abstractions
{
    public interface IRateLimitingValidator
    {
        public Task<RateLimitingValidationResponse> ValidateContext(RateLimitingValidationContext rateLimitingValidationContext);
    }
}