using Brolic.Abstractions;
using Brolic.Features.RateLimiting.Options;

namespace Brolic.Features.RateLimiting.Extensions
{
    public static class BrolicRouteExtensions
    {
        public static RateLimitingRouteOptions GetRateLimitingRouteOptions(this BrolicRoute brolicRoute)
        {
            return brolicRoute.Data["RateLimiting"] as RateLimitingRouteOptions;
        }
    }
}