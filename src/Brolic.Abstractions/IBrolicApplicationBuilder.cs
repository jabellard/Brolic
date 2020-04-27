using System;

namespace Brolic.Abstractions
{
    public interface IBrolicApplicationBuilder
    {
        IBrolicApplicationBuilder Use(Func<BrolicTrafficDelegate, BrolicTrafficDelegate> middleware);

        IBrolicApplicationBuilder UseMiddleware<TBrolicMiddleware>()
            where TBrolicMiddleware : IBrolicMiddleware;

        IBrolicApplicationBuilder UseMiddleware(Type middlewareType);
        BrolicTrafficDelegate Build();
    }
}