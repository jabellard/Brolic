using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic
{
    public class BrolicApplicationBuilder: IBrolicApplicationBuilder
    {
        private readonly IList<Func<BrolicTrafficDelegate, BrolicTrafficDelegate>> _middlewares;
        public IServiceProvider ApplicationServices { get; }

        public BrolicApplicationBuilder(IServiceProvider applicationServices)
        {
            ApplicationServices = applicationServices;
            _middlewares = new List<Func<BrolicTrafficDelegate, BrolicTrafficDelegate>>();
        }
        public IBrolicApplicationBuilder Use(Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        public IBrolicApplicationBuilder UseMiddleware<TBrolicMiddleware>()
            where TBrolicMiddleware : IBrolicMiddleware
        {
            return UseMiddleware(typeof(TBrolicMiddleware));
        }
        
        public IBrolicApplicationBuilder UseMiddleware(Type middlewareType)
        {
            return Use(next =>
            {
                var brolicMiddlewareProvider = ApplicationServices.GetService<IBrolicMiddlewareProvider>();
                var brolicMiddleware = brolicMiddlewareProvider.GetMiddleware(middlewareType);
                return async trafficContext => await brolicMiddleware.Invoke(trafficContext, next);
            });
        }

        public BrolicTrafficDelegate Build()
        {
            BrolicTrafficDelegate application = trafficContext =>
            {
                trafficContext.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            };
            
            foreach (var middleware in _middlewares.Reverse())
                application = middleware(application);

            return application;
        }
    }
}