using System;
using Brolic.Abstractions;
using Brolic.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBrolic(this IApplicationBuilder applicationBuilder, Action<IBrolicApplicationConfigurator> configuration)
        {
            var brolicApplicationConfigurator = new BrolicApplicationConfigurator(applicationBuilder.ApplicationServices);

            var routeConfigurationProvider = applicationBuilder.ApplicationServices
                    .GetRequiredService<IRouteConfigurationProvider>();
            var routeBuilderConfiguration = routeConfigurationProvider.GetRouteConfiguration();
            var routeBuilder = new RouteBuilder(applicationBuilder);
            routeBuilderConfiguration(routeBuilder);
            var router = routeBuilder.Build();
            brolicApplicationConfigurator
                .WithMiddleware<TrafficRoutingMiddleware>(PipelineComponents.TrafficRouting, router)
                .WithMiddleware<DownstreamMatchingMiddleware>(PipelineComponents.DownstreamMatching)
                .WithMiddleware<TrafficDispatchingMiddleware>(PipelineComponents.TrafficDispatching);


            var features = applicationBuilder
                .ApplicationServices
                .GetServices<IFeature>();
            foreach (var feature in features)
                feature.Configure(brolicApplicationConfigurator);

            configuration(brolicApplicationConfigurator);
            
            var brolicApplicationBuilder = new BrolicApplicationBuilder(applicationBuilder.ApplicationServices);
            var brolicApplicationConfiguration = brolicApplicationConfigurator.Configure();
            var trafficDelegate = brolicApplicationBuilder
                .ConfigurePipeline(brolicApplicationConfiguration)
                .Build();
            
            
            applicationBuilder.Use(async (httpContext, task) =>
            {
                var trafficContext = new TrafficContext(httpContext);
                await trafficDelegate.Invoke(trafficContext);
            });
            
            return applicationBuilder;
        }
    }
}