using Brolic.Abstractions;
using Brolic.Features.Null.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Features.Null
{
    public class NullFeature: IFeature
    {
        public string Key => "Null";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INullResponseWriter, NullResponseWriter>();
            services.AddSingleton<NullTrafficHandler>();
        }

        public void Configure(IBrolicApplicationConfigurator brolicApplicationConfigurator)
        {
            var trafficHandlerRegistrar = brolicApplicationConfigurator
                .ApplicationServices
                .GetRequiredService<ITrafficHandlerRegistrar>();
            
            trafficHandlerRegistrar.RegisterTrafficHandler<NullTrafficHandler>(Key);
        }
    }
}