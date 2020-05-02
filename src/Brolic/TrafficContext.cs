using Brolic.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic
{
    public class TrafficContext: ITrafficContext
    {
        public HttpContext HttpContext { get; }
        public Downstream Downstream => HttpContext.Items["Downstream"] as Downstream;

        public TrafficContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
    }
}