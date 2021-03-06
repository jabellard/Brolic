using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.RateLimiting.Abstractions;
using Brolic.Features.RateLimiting.Extensions;
using Microsoft.AspNetCore.Http;

namespace Brolic.Features.RateLimiting
{
    public class RateLimitingMiddleware: IBrolicMiddleware
    {
        private readonly ITrafficInitiatorIdentifier _trafficInitiatorIdentifier;
        private readonly IRateLimitingValidationStrategy _rateLimitingValidationStrategy;

        public RateLimitingMiddleware(ITrafficInitiatorIdentifier trafficInitiatorIdentifier,
            IRateLimitingValidationStrategy rateLimitingValidationStrategy)
        {
            _trafficInitiatorIdentifier = trafficInitiatorIdentifier;
            _rateLimitingValidationStrategy = rateLimitingValidationStrategy;
        }
        
        public async Task Invoke(ITrafficContext trafficContext, BrolicTrafficDelegate next)
        {
            var trafficInitiatorId = await _trafficInitiatorIdentifier.IdentifyTrafficInitiator(trafficContext);
            var httpContext = trafficContext.HttpContext;
            var httpMethod = httpContext.Request.Method;
            var rateLimitingRouteOptions = trafficContext.Route.GetRateLimitingRouteOptions();
            var applicableRateLimitingRules = rateLimitingRouteOptions.Rules
                .Where(r => !r.Methods.Any() || r.Methods.Contains(httpMethod));
            
            var rateLimitingValidationResponses = new List<RateLimitingValidationResponse>();
            foreach (var applicableRateLimitingRule in applicableRateLimitingRules)
            {
                var path = httpContext.Request.Path;
                var parsedMethod = !applicableRateLimitingRule.Methods.Any()
                    ? "Any"
                    : applicableRateLimitingRule.Methods.Aggregate(string.Empty, (current, method) =>
                    {
                        current += method;
                        return current;
                    });

                var rateLimitingValidationContext = new RateLimitingValidationContext
                {
                    TrafficInitiatorId = trafficInitiatorId,
                    Path = path,
                    Method = parsedMethod,
                    Limit = applicableRateLimitingRule.Limit,
                    Period = applicableRateLimitingRule.Period
                };
                var rateLimitingValidationResponse = await _rateLimitingValidationStrategy.ValidateContext(rateLimitingValidationContext);
                rateLimitingValidationResponses.Add(rateLimitingValidationResponse);
            }

            if (rateLimitingValidationResponses.Any(r => !r.IsValid))
            {
                httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return;
            }
            
            await next(trafficContext);
        }
    }
}