using System;
using System.Linq;
using Brolic.Abstractions;

namespace Brolic.Extensions
{
    public static class BrolicApplicationBuilderExtensions
    {
        public static IBrolicApplicationBuilder ConfigurePipeline(this IBrolicApplicationBuilder brolicApplicationBuilder, IBrolicApplicationConfiguration brolicApplicationConfiguration)
        {
            var middlewareRegistrations = brolicApplicationConfiguration.MiddlewareRegistrations;

            var preTrafficRoutingMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Pre{PipelineComponents.TrafficRouting}/"))
                .Select(r => r.Value);
            foreach (var preTrafficRoutingMiddlewareRegistration in preTrafficRoutingMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(preTrafficRoutingMiddlewareRegistration);
            }
            
            brolicApplicationBuilder.UseRegistration(middlewareRegistrations[PipelineComponents.TrafficRouting]);
            
            var postTrafficRoutingMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Post{PipelineComponents.TrafficRouting}/"))
                .Select(r => r.Value);
            foreach (var postTrafficRoutingMiddlewareRegistration in postTrafficRoutingMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(postTrafficRoutingMiddlewareRegistration);
            }
            
            var preDownstreamMatchingMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Pre{PipelineComponents.DownstreamMatching}/"))
                .Select(r => r.Value);
            foreach (var preDownstreamMatchingMiddlewareRegistration in preDownstreamMatchingMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(preDownstreamMatchingMiddlewareRegistration);
            }
            
            brolicApplicationBuilder.UseRegistration(middlewareRegistrations[PipelineComponents.DownstreamMatching]);
            
            var postDownstreamMatchingMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Post{PipelineComponents.DownstreamMatching}/"))
                .Select(r => r.Value);
            foreach (var postDownstreamMatchingMiddlewareRegistration in postDownstreamMatchingMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(postDownstreamMatchingMiddlewareRegistration);
            }
            
            var preTrafficDispatchingMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Pre{PipelineComponents.TrafficDispatching}/"))
                .Select(r => r.Value);
            foreach (var preTrafficDispatchingMiddlewareRegistration in preTrafficDispatchingMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(preTrafficDispatchingMiddlewareRegistration);
            }
            
            brolicApplicationBuilder.UseRegistration(middlewareRegistrations[PipelineComponents.TrafficDispatching]);
            
            var postTrafficDispatchingMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Post{PipelineComponents.TrafficDispatching}/"))
                .Select(r => r.Value);
            foreach (var postTrafficDispatchingMiddlewareRegistration in postTrafficDispatchingMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(postTrafficDispatchingMiddlewareRegistration);
            }
            
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