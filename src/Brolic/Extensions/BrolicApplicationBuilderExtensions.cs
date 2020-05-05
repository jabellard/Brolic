using System;
using System.Collections.Generic;
using System.Linq;
using Brolic.Abstractions;

namespace Brolic.Extensions
{
    public static class BrolicApplicationBuilderExtensions
    {
        public static IBrolicApplicationBuilder ConfigurePipeline(this IBrolicApplicationBuilder brolicApplicationBuilder, IBrolicApplicationConfiguration brolicApplicationConfiguration)
        {
            var middlewareRegistrations = brolicApplicationConfiguration.MiddlewareRegistrations;
            brolicApplicationBuilder
                .UseRegistrationSection(middlewareRegistrations,PipelineComponents.TrafficRouting)
                .UseRegistrationSection(middlewareRegistrations,PipelineComponents.DownstreamMatching)
                .UseRegistrationSection(middlewareRegistrations,PipelineComponents.TrafficDispatching);

            return brolicApplicationBuilder;
        }
        
        private static IBrolicApplicationBuilder UseRegistrationSection(
            this IBrolicApplicationBuilder brolicApplicationBuilder, IDictionary<string, MiddlewareRegistration> middlewareRegistrations, string sectionKey)
        {
            var preMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Pre{sectionKey}/"))
                .OrderBy(r => r.Value.Timestamp)
                .Select(r => r.Value);
            foreach (var preMiddlewareRegistration in preMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(preMiddlewareRegistration);
            }
            
            brolicApplicationBuilder.UseRegistration(middlewareRegistrations[sectionKey]);
            
            var postMiddlewareRegistrations = middlewareRegistrations
                .Where(r => r.Key.Contains($"Post{sectionKey}/"))
                .OrderBy(r => r.Value.Timestamp)
                .Select(r => r.Value);
            foreach (var postMiddlewareRegistration in postMiddlewareRegistrations)
            {
                brolicApplicationBuilder.UseRegistration(postMiddlewareRegistration);
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