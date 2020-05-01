using System;

namespace Brolic.Abstractions
{
    public interface IBrolicApplicationConfigurator
    {
        IServiceProvider ApplicationServices { get;}
        IBrolicApplicationConfiguration Configuration { get; }

        IBrolicApplicationConfigurator WithMiddleware(string key,
            Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware);

        IBrolicApplicationConfigurator WithMiddleware<TBrolicMiddleware>(string key, params object[] parameters)
            where TBrolicMiddleware : IBrolicMiddleware;

        IBrolicApplicationConfiguration Configure();
    }
}