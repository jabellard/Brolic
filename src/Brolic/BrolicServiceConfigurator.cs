using System;
using System.Collections.Generic;
using Brolic.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic
{
    public class BrolicServiceConfigurator: IBrolicServiceConfigurator
    {
        public IServiceCollection Services { get; }
        private readonly Lazy<ServiceProvider> _lazyServiceProvider;
        private readonly Dictionary<string, Type> _featureTypes;
        private ServiceProvider ServiceProvider => _lazyServiceProvider.Value;

        public BrolicServiceConfigurator(IServiceCollection services)
        {
            Services = services;
            _lazyServiceProvider = new Lazy<ServiceProvider>(services.BuildServiceProvider);
            _featureTypes = new Dictionary<string, Type>();
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
            _featureTypes.Add(featureType.FullName, featureType);
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

            foreach (var (key, value) in _featureTypes)
            {
                var featureInstance = ActivatorUtilities.CreateInstance(ServiceProvider, value) as IFeature;
                featureInstance.ConfigureServices(Services);
                Services.AddSingleton(featureInstance);
            }
            
            if ( _lazyServiceProvider.IsValueCreated)
                _lazyServiceProvider.Value.Dispose();
            
        }
    }
}