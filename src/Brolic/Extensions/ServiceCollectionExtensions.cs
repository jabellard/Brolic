using System;
using Brolic.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBrolic(this IServiceCollection services, Action<IBrolicServiceConfigurator> configuration)
        {
            var configurator = new BrolicServiceConfigurator(services);
            configuration(configurator);
            configurator.Configure();
            return services;
        }
    }
}