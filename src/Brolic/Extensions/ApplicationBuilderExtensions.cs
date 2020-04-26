using Brolic.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBrolic(this IApplicationBuilder applicationBuilder)
        {
            var features = applicationBuilder
                .ApplicationServices
                .GetServices<IFeature>();

            foreach (var feature in features)
                feature.Configure(applicationBuilder);
            
            applicationBuilder.UseMiddleware<BrolicMiddleware>();
            return applicationBuilder;
        }
    }
}