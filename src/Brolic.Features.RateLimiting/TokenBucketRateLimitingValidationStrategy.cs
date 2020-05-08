using System;
using System.Threading.Tasks;
using Brolic.Features.RateLimiting.Abstractions;
using Brolic.Features.RateLimiting.Extensions;

namespace Brolic.Features.RateLimiting
{
    public class TokenBucketRateLimitingValidationStrategy: IRateLimitingValidationStrategy
    {
        private readonly IRateLimitingCounterKeyBuilder _rateLimitingCounterKeyBuilder;
        private readonly IRateLimitingCounterStore _rateLimitingCounterStore;

        public TokenBucketRateLimitingValidationStrategy(IRateLimitingCounterKeyBuilder rateLimitingCounterKeyBuilder,
            IRateLimitingCounterStore rateLimitingCounterStore)
        {
            _rateLimitingCounterKeyBuilder = rateLimitingCounterKeyBuilder;
            _rateLimitingCounterStore = rateLimitingCounterStore;
        }
        
        public async Task<RateLimitingValidationResponse> ValidateContext(RateLimitingValidationContext rateLimitingValidationContext)
        {
            var rateLimitingCounter = new RateLimitingCounter
            {
                Timestamp = DateTime.UtcNow,
                Count = 1
            };
            var expirationTime = rateLimitingValidationContext.Period.ToTimeSpan();
            
            var counterKey = await _rateLimitingCounterKeyBuilder.BuildCounterKey(rateLimitingValidationContext);
            var rateLimitingCounterEntry = await _rateLimitingCounterStore.GetCounter(counterKey);
            if (rateLimitingCounterEntry != null)
            {
                rateLimitingCounter = rateLimitingCounterEntry;
                rateLimitingCounter.Count += 1;
                var expirationDateTime = rateLimitingCounterEntry.Timestamp + rateLimitingValidationContext.Period.ToTimeSpan();
                expirationTime = expirationDateTime - DateTime.UtcNow;
            }
            await _rateLimitingCounterStore.SetCounter(counterKey, rateLimitingCounterEntry, expirationTime);
            var rateLimitingValidationResponse = new RateLimitingValidationResponse
            {
                RateLimitingValidationContext = rateLimitingValidationContext,
                RateLimitingCounter = rateLimitingCounter

            };
            return rateLimitingValidationResponse;
        }
    }
}