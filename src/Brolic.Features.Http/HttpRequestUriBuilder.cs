using System;
using System.Linq;
using System.Threading.Tasks;
using Brolic.Abstractions;
using Brolic.Features.Http.Abstractions;
using Brolic.Features.Http.Extensions;

namespace Brolic.Features.Http
{
    public class HttpRequestUriBuilder: IHttpRequestUriBuilder
    {
        public Task<Uri> BuildHttpRequestUri(ITrafficContext trafficContext)
        {
            var httpDownstreamOptions = trafficContext.Downstream.GetHttpDownstreamOptions();
            var downstreamPath = httpDownstreamOptions.BasePath;
            if (downstreamPath.Last() != '/')
                downstreamPath += "/";
            var httpRequest = trafficContext.HttpContext.Request;
            var route = trafficContext.Route;
            if (route.CatchAll)
            {
                var catchAllPathSegment = httpRequest
                    .RouteValues
                    .First(rv => rv.Key == "catchAllPathSegment")
                    .Value;
                downstreamPath += catchAllPathSegment;
            }
            else if (route.PathSegments.Any())
                downstreamPath = route.PathSegments.Aggregate(downstreamPath, (current, pathSegment) =>
                {
                    if (current.Last() != '/')
                        current += "/";
                    var pathSegmentValue = httpRequest
                        .RouteValues
                        .First(rv => rv.Key == pathSegment)
                        .Value;
                    current += pathSegmentValue;
                    return current;
                });
            var queryString = httpRequest.QueryString.Value;
            downstreamPath += queryString;
            return Task.FromResult(new Uri(downstreamPath));
        }
    }
}