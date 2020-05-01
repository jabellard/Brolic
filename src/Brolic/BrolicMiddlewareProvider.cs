using System;
using Brolic.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic
{
    public class BrolicMiddlewareProvider: IBrolicMiddlewareProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public BrolicMiddlewareProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IBrolicMiddleware GetMiddleware(Type middlewareType, params object[] parameters)
        {
            return ActivatorUtilities.CreateInstance(_serviceProvider, middlewareType, parameters) as IBrolicMiddleware;
        }
    }
}