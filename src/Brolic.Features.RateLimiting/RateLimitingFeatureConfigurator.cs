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
        
        RateLimitingFeatureConfigurator WithCounterKeyBuilder<TRateLimitingCounterKeyBuilder>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TRateLimitingCounterKeyBuilder: IRateLimitingCounterKeyBuilder
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(IRateLimitingCounterKeyBuilder), typeof(TRateLimitingCounterKeyBuilder), serviceLifetime);
            Services.Add(serviceDescriptor);
            return this;
        }
        
        RateLimitingFeatureConfigurator WithCounterStore<TRateLimitingCounterStore>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TRateLimitingCounterStore: IRateLimitingCounterStore
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(IRateLimitingCounterStore), typeof(TRateLimitingCounterStore), serviceLifetime);
            Services.Add(serviceDescriptor);
            return this;
        }

        RateLimitingFeatureConfigurator WithValidationStrategy<TRateLimitingValidationStrategy>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TRateLimitingValidationStrategy: IRateLimitingValidationStrategy
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(IRateLimitingValidationStrategy), typeof(TRateLimitingValidationStrategy), serviceLifetime);
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