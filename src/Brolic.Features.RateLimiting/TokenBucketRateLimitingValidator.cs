using System.Threading.Tasks;
using Brolic.Features.RateLimiting.Abstractions;

namespace Brolic.Features.RateLimiting
{
    public class TokenBucketRateLimitingValidator: IRateLimitingValidator
    {
        public Task<RateLimitingValidationResponse> ValidateContext(RateLimitingValidationContext rateLimitingValidationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}