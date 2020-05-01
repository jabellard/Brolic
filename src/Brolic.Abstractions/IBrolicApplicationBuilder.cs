using System;

namespace Brolic.Abstractions
{
    public interface IBrolicApplicationBuilder
    {
        IBrolicApplicationBuilder Use(Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware);

        IBrolicApplicationBuilder UseMiddleware<TBrolicMiddleware>(params object[] parameters)
            where TBrolicMiddleware : IBrolicMiddleware;

        IBrolicApplicationBuilder UseMiddleware(Type middlewareType, params object[] parameters);
        BrolicTrafficDelegate Build();
    }
}