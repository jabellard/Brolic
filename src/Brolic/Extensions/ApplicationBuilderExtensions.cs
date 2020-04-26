using System;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBrolic(this IApplicationBuilder applicationBuilder, Action<IBrolicApplicationConfigurator> configuration)
        {
            var brolicApplicationConfigurator = new BrolicApplicationConfigurator(applicationBuilder.ApplicationServices);
            configuration(brolicApplicationConfigurator);
            
            var features = applicationBuilder
                .ApplicationServices
                .GetServices<IFeature>();

            foreach (var feature in features)
                feature.Configure(brolicApplicationConfigurator);
            
            var brolicApplicationBuilder = new BrolicApplicationBuilder();
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