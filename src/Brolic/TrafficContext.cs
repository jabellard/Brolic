using Brolic.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic
{
    public class TrafficContext: ITrafficContext
    {
        public HttpContext HttpContext { get; }
        
        public TrafficContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
    }
}