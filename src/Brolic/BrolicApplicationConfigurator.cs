using System;
using Brolic.Abstractions;

namespace Brolic
{
    public class BrolicApplicationConfigurator: IBrolicApplicationConfigurator
    {
        
        public IServiceProvider ApplicationServices { get;}
        public IBrolicApplicationConfiguration Configuration { get; }

        public BrolicApplicationConfigurator(IServiceProvider applicationServices)
        {
            ApplicationServices = applicationServices;
            Configuration = new BrolicApplicationConfiguration();
        }
        
        public IBrolicApplicationConfigurator WithMiddleware(string key,
            Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware)
        {
            Configuration.MiddlewareRegistrations.Add(key, new MiddlewareRegistration
            {
                Middleware = middleware,
                Type = MiddlewareType.Delegate
            });
            return this;
        }

        public IBrolicApplicationConfigurator WithMiddleware<TBrolicMiddleware>(string key, params object[] parameters)
            where TBrolicMiddleware : IBrolicMiddleware
        {
            Configuration.MiddlewareRegistrations.Add(key, new MiddlewareRegistration
            {
                Middleware = typeof(TBrolicMiddleware),
                Type = MiddlewareType.Class,
                Parameters = parameters
            });
            return this;
        }
        
        public IBrolicApplicationConfiguration Configure()
        {
            return Configuration;
        }
    }
}