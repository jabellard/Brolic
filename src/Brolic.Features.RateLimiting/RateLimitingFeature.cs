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
            services.TryAddSingleton<IRateLimitingCounterKeyBuilder, Sha1RateLimitingCounterKeyBuilder>();
            services.TryAddSingleton<IRateLimitingCounterStore, MemoryCacheRateLimitingCounterStore>();
            services.TryAddSingleton<IRateLimitingValidationStrategy, TokenBucketRateLimitingValidationStrategy>();
            services.TryAddSingleton<ITrafficInitiatorIdentifier, IpTrafficInitiatorIdentifier>();
        }

        public void Configure(IBrolicApplicationConfigurator brolicApplicationConfigurator)
        {
            brolicApplicationConfigurator.WithPostMiddleware<RateLimitingMiddleware>(PipelineComponents
                .DownstreamMatching);
        }
    }
}