using Brolic.Abstractions;
using Brolic.Features.Null.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Features.Null
{
    public class NullFeature: IFeature
    {
        public string Key => "Null";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INullResponseWriter, NullResponseWriter>();
        }

        public void Configure(IApplicationBuilder applicationBuilder)
        {
            var trafficHandlerRegistrar = applicationBuilder
                .ApplicationServices
                .GetRequiredService<ITrafficHandlerRegistrar>();
            
            trafficHandlerRegistrar.RegisterTrafficHandler<NullTrafficHandler>(Key);
        }
    }
}