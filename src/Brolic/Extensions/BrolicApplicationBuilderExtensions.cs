using System;
using Brolic.Abstractions;

namespace Brolic.Extensions
{
    public static class BrolicApplicationBuilderExtensions
    {
        public static IBrolicApplicationBuilder ConfigurePipeline(this IBrolicApplicationBuilder brolicApplicationBuilder, IBrolicApplicationConfiguration brolicApplicationConfiguration)
        {
            var middlewareRegistrations = brolicApplicationConfiguration.MiddlewareRegistrations;
            brolicApplicationBuilder.UseRegistration(middlewareRegistrations[PipelineComponents.TrafficRouting]);
            brolicApplicationBuilder.UseRegistration(middlewareRegistrations[PipelineComponents.TrafficDispatching]);
            return brolicApplicationBuilder;
        }

        private static IBrolicApplicationBuilder UseRegistration(
            this IBrolicApplicationBuilder brolicApplicationBuilder, MiddlewareRegistration middlewareRegistration)
        {
            switch (middlewareRegistration.Type)
            {
                case MiddlewareType.Delegate:
                    brolicApplicationBuilder.Use(
                        middlewareRegistration.Middleware as Func<BrolicTrafficDelegate, BrolicTrafficDelegate>);
                    break;
                case MiddlewareType.Class:
                    brolicApplicationBuilder.UseMiddleware(middlewareRegistration.Middleware as Type, middlewareRegistration.Parameters);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return brolicApplicationBuilder;
        }
    }
}