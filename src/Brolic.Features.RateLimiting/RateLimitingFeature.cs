using Brolic.Abstractions;
using Brolic.Features.RateLimiting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Brolic.Features.RateLimiting
{
    public class RateLimitingFeature: IFeature
    {
        public string Key => "RateLimiting";
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IRateLimitingValidator, TokenBucketRateLimitingValidator>();
            services.TryAddSingleton<ITrafficInitiatorIdentifier, IpTrafficInitiatorIdentifier>();
        }

        public void Configure(IBrolicApplicationConfigurator brolicApplicationConfigurator)
        {
            brolicApplicationConfigurator.WithPostMiddleware<RateLimitingMiddleware>(PipelineComponents
                .DownstreamMatching);
        }
    }
}