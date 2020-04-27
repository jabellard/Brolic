using System.Collections.Generic;

namespace Brolic.Abstractions
{
    public interface IBrolicApplicationConfiguration
    {
        IDictionary<string, MiddlewareRegistration> MiddlewareRegistrations { get; }
    }
}