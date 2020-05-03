using System;
using Brolic.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic
{
    public class BrolicServiceConfigurator: IBrolicServiceConfigurator
    {
        public IServiceCollection Services { get; }
        private readonly Lazy<ServiceProvider> _lazyServiceProvider;
        private ServiceProvider ServiceProvider => _lazyServiceProvider.Value;

        public BrolicServiceConfigurator(IServiceCollection services)
        {
            Services = services;
            _lazyServiceProvider = new Lazy<ServiceProvider>(services.BuildServiceProvider);
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
            var feature = ActivatorUtilities.CreateInstance(ServiceProvider, typeof(TFeature)) as IFeature;
            feature.ConfigureServices(Services);
            Services.AddSingleton(feature);
            return this;
        }

        internal void Configure()
        {
            //TODO: Add services
            ServiceProvider.Dispose();
        }
    }
}