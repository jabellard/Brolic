using System;
using System.Collections.Generic;
using System.Linq;
using Brolic.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic
{
    public class BrolicServiceConfigurator: IBrolicServiceConfigurator
    {
        public IServiceCollection Services { get; }
        private readonly Lazy<ServiceProvider> _lazyServiceProvider;
        private readonly Dictionary<string, Tuple<Type, DateTime>> _keyedFeatureTypeRegistrations;
        private ServiceProvider ServiceProvider => _lazyServiceProvider.Value;

        public BrolicServiceConfigurator(IServiceCollection services)
        {
            Services = services;
            _lazyServiceProvider = new Lazy<ServiceProvider>(services.BuildServiceProvider);
            _keyedFeatureTypeRegistrations = new Dictionary<string, Tuple<Type, DateTime>>();
        }
        
        public IBrolicServiceConfigurator WithOptions(IConfigurationSection configuration)
        {
            Services.Configure<BrolicOptions>(configuration);
            return this;
        }

        public IBrolicServiceConfigurator WithOptions(Action<BrolicOptions> configuration)
        {
            Services.Configure(configuration);
            return this;
        }
        
        public IBrolicServiceConfigurator WithFeature<TFeature>()
            where TFeature : IFeature
        {
            var featureType = typeof(TFeature);
            _keyedFeatureTypeRegistrations.Add(featureType.FullName, new Tuple<Type, DateTime>(featureType, DateTime.UtcNow));
            return this;
        }

        internal void Configure()
        {
            Services.AddOptions();
            Services.AddLogging();
            Services.AddRouting();
            Services.AddSingleton<IBrolicMiddlewareProvider, BrolicMiddlewareProvider>();
            Services.Configure<BrolicOptions>(options => { });
            Services.AddSingleton<IFeatureOptionsProvider, FeatureOptionsProvider>();
            Services.AddSingleton<IRouteConfigurationProvider, RouteConfigurationProvider>();
            Services.AddSingleton<ITrafficHandlerRegistrar, TrafficHandlerRegistrar>();
            Services.AddSingleton<ITrafficHandlerProvider, TrafficHandlerProvider>();
            Services.AddSingleton<IFeatureRegistration, FeatureRegistration>();
            Services.AddSingleton<IFeatureProvider, FeatureProvider>();

            var featureRegistrations = _keyedFeatureTypeRegistrations
                .Select(ktr => new FeatureRegistration
                {
                    Feature = ActivatorUtilities.CreateInstance(ServiceProvider, ktr.Value.Item1) as IFeature,
                    Timestamp = ktr.Value.Item2
                })
                .ToList();
            
            featureRegistrations
                .ForEach(fr =>
                {
                    fr.Feature.ConfigureServices(Services);
                    Services.AddSingleton<IFeatureRegistration>(fr);
                });
            
            if ( _lazyServiceProvider.IsValueCreated)
                _lazyServiceProvider.Value.Dispose();
            
        }
    }
}