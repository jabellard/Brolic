using System;
using System.Linq;
using System.Threading.Tasks;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Brolic
{
    public class RouteConfigurationProvider: IRouteConfigurationProvider
    {
        private readonly BrolicOptions _brolicOptions;

        public RouteConfigurationProvider(IOptions<BrolicOptions> optionsAccessor)
        {
            _brolicOptions = optionsAccessor.Value;
        }
        public Action<IRouteBuilder> GetRouteConfiguration()
        {
            return routeBuilder =>
            {
                foreach (var route in _brolicOptions.Routes)
                {
                    var pathTemplate = route.PathTemplate;
                    if (pathTemplate.Last() != '/')
                        pathTemplate += "/";
                    pathTemplate += "{*downstreamUriSegment}";
                    
                    var downstream = _brolicOptions.Downstreams[route.Downstream];
                    routeBuilder.MapRoute(pathTemplate, httpContext =>
                    {
                        httpContext.Items.Add("Downstream", downstream);
                        return Task.CompletedTask;
                    });
                }
            };
        }
    }
}