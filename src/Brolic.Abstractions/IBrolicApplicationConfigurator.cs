using System;

namespace Brolic.Abstractions
{
    public interface IBrolicApplicationConfigurator
    {
        IServiceProvider ApplicationServices { get;}
        IBrolicApplicationConfiguration Configuration { get; }

        IBrolicApplicationConfigurator WithPreMiddleware(string key,
            Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware);
        
        IBrolicApplicationConfigurator WithMiddleware(string key,
            Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware);
        
        IBrolicApplicationConfigurator WithPostMiddleware(string key,
            Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware);

        IBrolicApplicationConfigurator WithPreMiddleware<TBrolicMiddleware>(string key, params object[] parameters)
            where TBrolicMiddleware : IBrolicMiddleware;
        
        IBrolicApplicationConfigurator WithMiddleware<TBrolicMiddleware>(string key, params object[] parameters)
            where TBrolicMiddleware : IBrolicMiddleware;
        
        IBrolicApplicationConfigurator WithPostMiddleware<TBrolicMiddleware>(string key, params object[] parameters)
            where TBrolicMiddleware : IBrolicMiddleware;

        IBrolicApplicationConfiguration Configure();
    }
}