
using System;
using Brolic.Features.RateLimiting.Extensions;

namespace Brolic.Features.RateLimiting
{
    public class RateLimitingValidationResponse
    {
        public RateLimitingValidationContext RateLimitingValidationContext { get; set; }
        public RateLimitingCounter RateLimitingCounter { get; set; }
        public bool IsValid => RateLimitingCounter.Count <= RateLimitingValidationContext.Limit;

        public DateTime NextRefillTime =>
            RateLimitingCounter.Timestamp + RateLimitingValidationContext.Period.ToTimeSpan();
    }
}