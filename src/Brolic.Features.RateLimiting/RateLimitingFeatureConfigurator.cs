using Brolic.Abstractions;
using Brolic.Features.RateLimiting.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Features.RateLimiting
{
    public class RateLimitingFeatureConfigurator
    {
        public IBrolicServiceConfigurator BrolicServiceConfigurator { get; }
        public IServiceCollection Services => BrolicServiceConfigurator.Services;

        internal RateLimitingFeatureConfigurator(IBrolicServiceConfigurator brolicServiceConfigurator)
        {
            BrolicServiceConfigurator = brolicServiceConfigurator;
        }

        RateLimitingFeatureConfigurator WithRateLimitingValidator<TRateLimitingValidator>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TRateLimitingValidator: IRateLimitingValidator
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(IRateLimitingValidator), typeof(TRateLimitingValidator), serviceLifetime);
            Services.Add(serviceDescriptor);
            return this;
        }
        
        RateLimitingFeatureConfigurator WithTrafficInitiatorIdentifier<TTrafficInitiatorIdentifier>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TTrafficInitiatorIdentifier: ITrafficInitiatorIdentifier
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(ITrafficInitiatorIdentifier), typeof(TTrafficInitiatorIdentifier), serviceLifetime);
            Services.Add(serviceDescriptor);
            return this;
        }
        

        internal void Configure()
        {
            BrolicServiceConfigurator.WithFeature<RateLimitingFeature>();

        }
    }
}