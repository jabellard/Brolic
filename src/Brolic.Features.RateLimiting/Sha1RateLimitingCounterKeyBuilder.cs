using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Brolic.Features.RateLimiting.Abstractions;

namespace Brolic.Features.RateLimiting
{
    public class Sha1RateLimitingCounterKeyBuilder: IRateLimitingCounterKeyBuilder
    {
        public Task<string> BuildCounterKey(RateLimitingValidationContext rateLimitingValidationContext)
        {
            var counterKeyTemplate = $"{rateLimitingValidationContext.TrafficInitiatorId}_{rateLimitingValidationContext.Path}" +
                             $"{rateLimitingValidationContext.Method}_{rateLimitingValidationContext.Limit}" +
                             $"{rateLimitingValidationContext.Period}";
            var counterKeyBytes = Encoding.UTF8.GetBytes(counterKeyTemplate);
            using var hashingAlgorithm = SHA1.Create();
            var counterKeyHashBytes = hashingAlgorithm.ComputeHash(counterKeyBytes);
            var counterKey = BitConverter.ToString(counterKeyHashBytes)
                .Replace("-", string.Empty);
            return Task.FromResult(counterKey);
        }
    }
}