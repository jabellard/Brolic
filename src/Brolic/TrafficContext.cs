using Brolic.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic
{
    public class TrafficContext: ITrafficContext
    {
        public HttpContext HttpContext { get; }
        public BrolicRoute Route => HttpContext.Items[ObjectKeys.HttpContextRouteObject] as BrolicRoute;
        public Downstream Downstream => HttpContext.Items[ObjectKeys.HttpContextDownstreamObject] as Downstream;

        public TrafficContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
    }
}