using System;
using Brolic.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic
{
    public class BrolicServiceConfigurator: IBrolicServiceConfigurator
    {
        public IServiceCollection Services { get; }

        public BrolicServiceConfigurator(IServiceCollection services)
        {
            Services = services;
        }
        
        public IBrolicServiceConfigurator WithFeature<TFeature>() where TFeature : IFeature
        {
            var feature = Activator.CreateInstance(typeof(TFeature)) as IFeature;
            feature.ConfigureServices(Services);
            Services.AddSingleton(feature);
            return this;
        }

        internal void Configure()
        {
            
        }
    }
}