using System.Collections.Generic;

namespace Brolic.Features.RateLimiting.Options
{
    public class RateLimitingRouteOptions
    {
        public List<RateLimitingRule> Rules { get; set; }
    }
}