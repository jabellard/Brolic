using System;
using Brolic.Abstractions;

namespace Brolic.Features.RateLimiting.Extensions
{
    public static class BrolicServiceConfiguratorExtensions
    {
        public static IBrolicServiceConfigurator WithRateLimitingFeature(this IBrolicServiceConfigurator brolicServiceConfigurator, Action<RateLimitingFeatureConfigurator> configuration = default)
        {
            var rateLimitingFeatureConfigurator = new RateLimitingFeatureConfigurator(brolicServiceConfigurator);
            configuration?.Invoke(rateLimitingFeatureConfigurator);
            rateLimitingFeatureConfigurator.Configure();
            return brolicServiceConfigurator;
        }
    }
}