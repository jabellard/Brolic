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
                    var pathTemplate = route.BasePath;
                    if (pathTemplate.Last() != '/')
                        pathTemplate += "/";
                    if (route.CatchAll)
                        pathTemplate += "{*catchAllPathSegment}";
                    else if (route.PathSegments.Any())
                        pathTemplate = route.PathSegments.Aggregate(pathTemplate, (current, pathSegment) =>
                        {
                            if (current.Last() != '/')
                                current += "/";
                            current += "{*" + pathSegment + "}";
                            return current;
                        });

                    var downstream = _brolicOptions.Downstreams[route.Downstream];
                    if (route.Methods.Any())
                        foreach (var method in route.Methods)
                            routeBuilder.MapVerb(method, pathTemplate, httpContext =>
                            {
                                httpContext.Items.Add(ObjectKeys.HttpContextRouteObject, route);
                                httpContext.Items.Add(ObjectKeys.HttpContextDownstreamObject, downstream);
                                return Task.CompletedTask;
                            });
                    else
                        routeBuilder.MapRoute(pathTemplate, httpContext =>
                        {
                            httpContext.Items.Add("Route", route);
                            httpContext.Items.Add("Downstream", downstream);
                            return Task.CompletedTask;
                        });
                }
            };
        }
    }
}