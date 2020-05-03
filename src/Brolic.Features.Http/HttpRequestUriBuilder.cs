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
            var httpRequest = trafficContext.HttpContext.Request;
            var httpDownstreamOptions = trafficContext.Downstream.GetHttpDownstreamOptions();
            var downstreamUri = httpDownstreamOptions.BaseUri;
            if (downstreamUri.Last() != '/')
                downstreamUri += "/";
            var downstreamUriSegment = httpRequest
                .RouteValues
                .First(rv => rv.Key == "downstreamUriSegment")
                .Value;
            downstreamUri += downstreamUriSegment;
            var queryString = httpRequest.QueryString.Value;
            downstreamUri += queryString;
            return Task.FromResult(new Uri(downstreamUri));
        }
    }
}