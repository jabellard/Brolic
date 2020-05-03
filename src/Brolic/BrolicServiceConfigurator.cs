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
        private readonly Dictionary<string, Type> _keyedFeatureTypes;
        private ServiceProvider ServiceProvider => _lazyServiceProvider.Value;

        public BrolicServiceConfigurator(IServiceCollection services)
        {
            Services = services;
            _lazyServiceProvider = new Lazy<ServiceProvider>(services.BuildServiceProvider);
            _keyedFeatureTypes = new Dictionary<string, Type>();
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
            _keyedFeatureTypes.Add(featureType.FullName, featureType);
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

            var featureInstances = _keyedFeatureTypes
                .Select(kt => ActivatorUtilities.CreateInstance(ServiceProvider, kt.Value) as IFeature)
                .ToList();
            
            featureInstances
                .ForEach(fi =>
                {
                    fi.ConfigureServices(Services);
                    Services.AddSingleton(fi);
                });
            
            if ( _lazyServiceProvider.IsValueCreated)
                _lazyServiceProvider.Value.Dispose();
            
        }
    }
}