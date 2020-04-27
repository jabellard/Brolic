using System;
using Brolic.Abstractions;

namespace Brolic
{
    public class BrolicMiddlewareProvider: IBrolicMiddlewareProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public BrolicMiddlewareProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IBrolicMiddleware GetMiddleware(Type middlewareType)
        {
            return _serviceProvider.GetService(middlewareType) as IBrolicMiddleware;
        }
    }
}