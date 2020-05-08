using System;

namespace Brolic.Features.RateLimiting
{
    public class RateLimitingCounter
    {
        public long Count { get; set; }
        public DateTime Timestamp { get; set; }
    }
}