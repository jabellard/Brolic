using System.Threading.Tasks;
using Brolic.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Brolic.Middlewares
{
    public class DownstreamMatchingMiddleware: IBrolicMiddleware
    {
        public async Task Invoke(ITrafficContext trafficContext, BrolicTrafficDelegate next)
        {
            var endPoint = trafficContext.HttpContext.GetEndpoint();
            endPoint.RequestDelegate(trafficContext.HttpContext);
            await next(trafficContext);
        }
    }
}