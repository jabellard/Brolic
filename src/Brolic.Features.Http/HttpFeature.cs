using Brolic.Abstractions;
using Brolic.Features.Http.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brolic.Features.Http
{
    public class HttpFeature: IFeature
    {
        public string Key => "Http";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpRequestUriBuilder, HttpRequestUriBuilder>();
            services.AddSingleton<IHttpRequestContentBuilder, HttpRequestContentBuilder>();
            services.AddSingleton<IHttpRequestBuilder, HttpRequestBuilder>();
            services.AddHttpClient<IHttpRequestExecutor, HttpRequestExecutor>();
            services.AddTransient<HttpFeature>();
        }

        public void Configure(IBrolicApplicationConfigurator brolicApplicationConfigurator)
        {
            var trafficHandlerRegistrar = brolicApplicationConfigurator
                .ApplicationServices
                .GetRequiredService<ITrafficHandlerRegistrar>();
            
            trafficHandlerRegistrar.RegisterTrafficHandler<HttpTrafficHandler>(Key);
        }
    }
}