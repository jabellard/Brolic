using System.Collections.Generic;
using Brolic.Abstractions;

namespace Brolic
{
    public class BrolicApplicationConfiguration: IBrolicApplicationConfiguration
    {
        public IDictionary<string, MiddlewareRegistration> MiddlewareRegistrations { get; }

        public BrolicApplicationConfiguration()
        {
            MiddlewareRegistrations = new Dictionary<string, MiddlewareRegistration>
            {
                {
                    PipelineComponents.Main,
                    new MiddlewareRegistration
                    {
                        Middleware = typeof(MainBrolicMiddleware),
                        MiddlewareType = MiddlewareType.Delegate
                    }
                }
            };
        }
    }
}