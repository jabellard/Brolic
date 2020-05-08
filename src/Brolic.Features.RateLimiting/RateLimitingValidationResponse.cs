
using System;

namespace Brolic.Features.RateLimiting
{
    public class RateLimitingValidationResponse
    {
        public RateLimitingValidationContext RateLimitingValidationContext { get; set; }
        public long Remaining { get; set; }
        public bool IsValid { get; set; }

        public DateTime NextRefillTime { get; set; }
    }
}