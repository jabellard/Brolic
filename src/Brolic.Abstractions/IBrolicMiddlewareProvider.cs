using System;

namespace Brolic.Abstractions
{
    public interface IBrolicMiddlewareProvider
    {
        IBrolicMiddleware GetMiddleware(Type middlewareType);
    }
}