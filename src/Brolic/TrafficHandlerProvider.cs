using System;
using Brolic.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic
{
    public class TrafficHandlerProvider: ITrafficHandlerProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITrafficHandlerRegistrar _trafficHandlerRegistrar;

        public TrafficHandlerProvider(IServiceProvider serviceProvider, ITrafficHandlerRegistrar trafficHandlerRegistrar)
        {
            _serviceProvider = serviceProvider;
            _trafficHandlerRegistrar = trafficHandlerRegistrar;
        }
        public ITrafficHandler GetTrafficHandler(string key)
        {
            var trafficHandlerType = _trafficHandlerRegistrar.ResolveTrafficHandler(key);
            return _serviceProvider.GetRequiredService(trafficHandlerType) as ITrafficHandler;
        }
    }
}